/*
 * Dois universos distintos
 *     Banco de dados usam o modelo relacional, baseado em tabelas e colunas
 *
 *     Objetos usam o modelo orientado a objetos,
 *         baseado em classes, fields, properties
 *     
 * Papel do ADO.NET Entity Framework
 *     A funcção do ADO.NET Entity Framework é juntar esses dois universos
            Mapeamento objeto relacional
       O desenvolvedor trabalha apenas com o modelo de objetos
       
       Todas as operações no banco de dados ficam a cargo do ADO.NET EF
                   
 * Entities
        Entities são classes que representam conceitos da aplicação, 
            que são mapeados para tabelas no banco de dados
          -O mapeamento pode ser totalmente customizado
          -Uma entidade não precisa ser necessariamente mapeada para apenas uma tabela
        
        Uma entidade é definida como um POCO(Plain Old CLR Object)
            Não precisa estender de outra classe;
            Possuir um construtor padrão; 
            Contém properties;  
                Properties que definem relacionamentos precisam ser definidas como VIRTUAL;
                
 * Camadas para o mapeamento
        A criação do mapeamento exige a definição de três camadas
            Lógica
                Definimos o modelo relacional
                Definida pela SSDL(Store Schema Definition Language)
            
            Conceitual
                Definição das entidades
                Definida pelo CSDL (Conceptual Schema Definition Language)
            
            Mapeamento
                Mapeamento entre o modelo relacional e o modelo de entidades
                Definido pelo MSL (Mapping Specification Language)
             
            Apenas o arquivo *.edmx importa
                Model.edmx
                    (SSDL
                     CSDL
                     MSL
                     DESIGNER) - Incorporados ao assembly gerado
                               - É possivel também manter os arquivos no diretório do projeto, fora do assembly                                    
 *
 * Abordagens para o ORM
        O ADO.NET suporta 3 abordagens para gerar esse mapeamento objeto relacional
            Model First
                As entidades são criadas primeiro
                O banco de dados é criado com base nas entidades
            
            Database First
                O banco de dados é gerado primeiro
                As entidades são geradas com base nas tabelas e colunas existentes no banco de dados 
            
            Code First
                As entidades são criadas primeiro
                Não existe a necessidade de configurar o mapeamento usando CSDL,SSDL E MSL
                        
 * A classe DbContext
        Um objeto desta classe é o ponto de entrada para o uso do ADO.NET EF
        public class AppEntities : DbContext Herda de DbContext
        {
                                        ConnectionString definida em App.config    
            public AppEntities():base("name=AppEntities"){}
            
            public DbSet<Estoque> Estoques {get; set;} property para acesso às entidades
            public DbSet<Produto> Produtos {get; set;} property para acesso às entidades
        }    
 * Manipulação de entidades
 
        Inserindo registros
                        Cria o objeto do contexto
            using(AppEntities context = new AppEntities())
            {
                                Cria o objeto
                Produto p = context.Produtos.Create();
                p.Nome = "Telefone";
                p.Descricao = "Telefone sem fio";
                p.Valor = 150;
                
                Estoque e = context.Estoques.Create();
                e.Nome = "Estoque 1";
                e.Tipo = "A";
                
                Aplica o relacionamento
                p.Estoque = e;
                
                Adiciona o produto no contexto
                context.Produtos.Add(p);
                
                Efetua a inserção no banco de dados
                context.SaveChanges();
            }     
            
        Alterando registros existentes no banco de dados
            using(AppEntities context = new AppEntities())
            {
                                Obtém a entidade com base na sua chave
                Produto p = context.Produtos.Find(2);
                p.Descricao = "Tv de LED";
                context.SaveChanges(); Atualiza a entidade no banco de dados
            }    
        
        Deletando registros do banco de dados
            using(AppEntities context = new AppEntities())
            {
                                Obtém a entidade com base na sua chave
                Produto p = context.Produtos.Find(3);
                context.Produtos.Remove(p); Remove a entidade do contexto;
                context.SaveChanges(); Efetiva a exclusão
            }
            
        Listando registros do banco de dados
            using(AppEntities context = new AppEntities())
            {
                foreach(var p in context.Produtos)
                {
                    Console.WriteLine(p.Nome);
                }
            }
            
 * Lazy loading e eager loading
        Quando existem relacionamentos entre entidades e estes relacionamentos precisam ser lidos, é possivel usar
            Lazy loading
                A leitura do relacionamento é feita apenas no momento do acesso à entidade
                
            Eager Loading
                A leitura dos dados do relacionamento é feita no momento que a "entidade pai" é carregada
        O Lazy Loading é usado por padrão, mas isto pode ser alterado
            context.Configuration.LazyLoadingEnabled = false;
        
        Lazy Loading
            using(AppEntities context = new AppEntities())
            {
                foreach(var p in context.Produtos)
                {
                    string nome = p.Estoque.Nome;
                                    Os dados do estoque são carregados no momento que a entidade é acessada
                }
            }  É realizada uma query para buscar os produtos e, para cada estoque, são executadas outras queries
            
        Eager Loading
            using(AppEntities context = new AppEntities())
            {
                                                Carregao relacionamento estoque ao carregar os produtos    
                foreach(var p in context.Produtos.Include("Estoque"))
                {
                    string nome = p.Estoque.Nome;
                }
            }  É realizada apenas uma query para buscar os produtos 
               que faz um join entre produtos e estoques e carrega todas as entidades
            
       Depende do caso:
        Considerar as vantagens e desvantagens de cada abordagem
            O lazy loading só carrega os dados quando eles forem usados, 
            mas pode aumentar o numero de queries executadas no banco de dados
            
            O Eager Loading diminue o numero de queries executadas no banco de dados, 
            mas pode trazer muita informação desnecessaria      
            
 * LINQ to entities
        A linguagem LINQ pode ser usada para extrair dados de banco de dados
            using(AppEntities context = new AppEntities())
            {
                var q = from p in context.Produtos
                where p.Estoque.Nome.EndsWith("A") Produtos que estão em estoques cujo o nome termina com "A"
                select p;
                
                foreach(var p in q)
                {
                    string nome = p.Nome;
                }
            }
 
 * Estados de entidades
        O objeto DbContext é capaz de gerenciar entidades
        Entidades que estão sob o controle do DbContext estão atachadas
        Apenas entidades atachadas são sincronizadas com o banco de dados
        Estados: 
            Added:     adicionada
            Deleted:   excluida
            Modified:  modificada
            Unchenged: não modificada
            Detached:  não está atachada ao contexto
            
  Cenário 1:
    Criação de uma nova entidade
        Produto p = context.Produtos.Create();
        p(detached) 
        
        context.Produtos.Add(p);
        p(Added)
        
        context.SaveChanges();
        p(Unchanged)
        
  Cenério 2:
    Carregamento e alteração de uma nova entidade:
        Produto p = context.Find(1);
        p(Unchanged)
        
        p.Nome = "Notebook";
        p(Modified)
        
        context.SaveChanges();
        p(Unchanged)      
   
   Qualquer operação de carregamento traz os dados para o contexto
   
   No caso de exclusão, funciona da mesma forma. 
    A diferença é que o estado muda para Deleted 
   
   Cenário 3:
    Carregamento e desatachamento de entidade:
        
        Produto p = context.Produtos.Find(1);
        p(unchanged)
        
        context.Entry(p).State = EntityState.Detached;
        p(detached)
        
        alterações feitas na entidade não vão mais ser sincronizadas 
        com o banco de dados na chamada SaveChanges();
        
   Cenário 4:
    Atachamento e entidade existente e modificar fora do contexto:
        Produto p = ... ;
        p(detached)
        
        context.Entry(p).State = EntityState.Modified;
        p(modified);
        
        context.SaveChanges(); 
        p(Unchanged)
 */
namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
        }
    }
}