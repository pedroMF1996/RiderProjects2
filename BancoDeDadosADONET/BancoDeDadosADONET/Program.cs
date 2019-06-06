/*
 * ADO.NET na conexão com banco de dados
 *     Conexão com banco de dados
 *         Aplicação dotnet
 *         ->
 *         ADO.NET {Abstrai o acesso aos mais diversos tipos de banco de dados}
 *         ->
 *         Banco de Dados
 * ADO.NET Data Providers
 *     É responsavel por permitir a comunicação com determinado SGBD
 *         Cada SGBD tem um provider especifico
 *
 *     A plataforma  .NET ja traz alguns providers nativos
 *         Microsoft SQL server
 *         ODBC
 *         OLEDB
 *
 *     Os providers estão disponiveis no assembly System.Data.dll
 * 
 *     Para integração com providers não nativos (oracle, mysql)
 *         tem que ir até a pagina do fabricante
 *         procurar na pagina o provider
 *         quem implementa o provider é o proprio fabricante
 *
 *     Um provider é uma implementação das classes do ado.net
 *    ADO.NET SQL server{
 *     DbConnection    
 *         SqlConnection
 *     DbCommand
 *         SqlCommand
 *     DbTransaction
 *         SqlTransaction}
 *        Esses objetos precisam ser fechados quando abertos
 *     ADO.NET oracle{
 *     DbConnection
        OracleConnection
 *     DbCommand
        OracleCommand
 *     DbTransaction
 *      OracleTransaction}
 *
 *     Conclusao: Independente do provider que você usa
 *                 as classes do ado.net não mudam, são as mesmas
 *                 as classes com as quais você terá de trabalhas na sua aplicação são as mesmas
 *                 independente dos bancos de dados que você está usando
 
        Provider Factory
            permite criar objetos de provider especifico
                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                                                                                nome do privider
                DbConnection conn = factory.CreateConnection(); Codigo independente de provider
                DbCommand cmd = factory.CreateCommand();
                
                SqlConnection conn = new SqlConnection(); código especifica do sql server
                SqlCommand cmd = new SqlCommand();
                
 * Trabalhando com conexões
 *     Obter um objeto da classe DbConnection
        
        using(DbConnection conn = factory.CreateConnection())objeto que representa a conexão
        {
            conn.ConnectionString = @"Data Source = (local)\SQLEXPRESS;
                Initial Catalog=testebd; Integrated Security=True"; connection string
            conn.Open();    
        }->será chamado o metodo disable no objeto conn quando o bloco unsig terminar 
            Sempre que abrir uma conexão, fechá-la
        
        Outra opção que se tem é usar o bloco try finally
        DbConnection conn = factory.CreateConnection();
        try
        {
            conn.Open();
        }
        finally
        {
            conn.Close();
        }
        
        Cada provider define um formato e informações para a connection string
        A classe ConnectionStringBuilder pode ser usada para facilitar a criação de uma connection string
        SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
        sb.DataSource = @"(local)\SQLEXPRESS";
        sb.InitialCatalog = "testebd";
        sb.IntegratedSecurity = true;
        
        conn.ConnectionString = sb.ConnectionString;
 
 *     Externalizando dados do provider
            Deixar informações sobre o provider diretamente no código não é uma boa prática
            O nome do provider e a connection string podem ir para um arquivo de configuração
                App.config
                
                <configuration>
                    <appSettings>
                        <add key="provider" value="System.Data.SqlClient"/>
                    </appSettings>
                    <connectionStrings>
                        <add name="db" connectionSring="Data Source=(local)\SQLEXPRESS;
                            Initial Catalog=testebd.Integrated Security=True"/>
                    </connectionStrings>
                </configuration>
            
            A classe ConfigurationManager é usada para ler as informações
                Assembly: System.Configuration.dll
                Namespace: System.Configuration
                
                string provider = ConfigurationManager.AppSettings["provider"];    
                string cn = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
 *
 * Execução de comandos
    Depois de estabelecida a conexão, o próximo passo é a criação de comandos,
    que serão executados no banco de dados
        INSERT,DELETE,UPDATE,SELECT
    Um objeto do tipo DbCommand é usado
        DbCommand cmd = factory.CreateCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT nome FROM contato";
            A conexão e a query devem estar associados ao comando
          
    A execução de um comando é feita através do seguinte método
    Método               Tipo de comando           Retorno
    ExecuteNonQuery()    INSERT, UPDATE, Delete    int
    ExecuteReader()      SELECT                    DbDataReader
    ExecuteScalar()      SELECT                    object
    
    cmd.CommandText = "INSERT INTO contato(nome) VALUES ('Maria')";
    int num = cmd.ExecuteNonQuery();
    num -> numero de registros afetados
    
    cmd.CommandText = "SELECT nome, idade, FROM contato";
    using(DbDataReader result = cmd.ExecuteReader())
    {
        while(result.Read()) enquanto houver registros 
        {
            string nome = (string)result["nome"]; 
            int idade = (int)result["idade"]; também é possivel colher os dados por indice
        }
    }
    
    
    cmd.CommandText = SELECT MAX(idade) FROM contato";
    object obj = cmd.ExecuteScalar(); retorna apenas um registro
    
    if(!Convert.IsDBNull(obj))verifica se é null
    {
        int max = (int)obj;
    } 
    
    DBNULL é uma classe que representa um valor nulo vindo do banco de dados
    if(count == DBNull.Value)
    {
        ...
    }
    
 * Comandos parametrizados
    
    Exemplo com concatenação
    void Inserir(string nome, int idade, DbConnection conn, DbProviderFactory factory)
    {
        using(DbCommand cmd = factory.CreateCommand())
        {
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO contato(nome, idade)
                VALUES('" + nome + "',"+ idade +")";
            cmd.ExecuteNonQuery();    
        }
    }
    A query é criada e enviada toda vez para o banco de dados, a fim de ser executada    
    A solução para este problema é criar um comando parametrizado
    
    Exemplo de comandos parametrizados
        
        void Inserir(string nome, int idade, DbConnection conn, DbProviderFactory factory)
        {
            using(DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO contato(nome, idade) VALUES(@Nome, @Idade)";
                cdm.Connection = conn;                                        placeholder
                
                DbParameter param = factory.CreateParameter(); parametros para substituir os placeholders
                param.ParameterName = "@Nome";
                param.Value = nome;
                cmd.Parameters.Add(param);
                
                
                param = factory.CreateParameter();
                param.ParameterName = "@Idade";
                param.Value = idade;
                cmd.Parameters.Add(param);
                
                cmd.ExecuteNonQuery();
            }
        }
        O parse da query ocorre apenas uma vez, o que acelera a sua execução
        
        O.NET converte o tipo de dado do c# em um tipo de dado entendido pelo banco de dados
        
        É possivel controlar a conversão via programação
            param.ParameterName = "@Nome";
            param.Value = nome;
            param.DbType = DbType.String; Conversão valida para qualquer provider
            
            param.ParameterName = "@Nome";
            param.Value = nome;
            param.DbType = SqlDbType.NVarChar; Conversão valida para sql server
  
        A documentação do ADO dotnet detalha como funcionam as conversões para os mais diversos tipos de bancos existentes
        
 * Tranzações
        Uma transação é uma operação atomica
            ou ela executa por completo, ou não executa
            não existe possibilidade de ela executar apenas parcialmente
        O exemplo mais classico de transação é uma transferencia bancaria de um valor
            Duas operações
                Saque na conta de origem
                Deposito na conta destino
            Ambas precisam executar de forma atomica
       
       Iniciando uma transação
        No ADO.NET, uma transação é iniciada através do método BeginTransaction(),
        da classe DbConnection;             
            DbTransaction transaction = conn.BeginTransaction();
                            objeto que representa a transação
        A transação termina com um commit ou um rollback;
            transaction.Commit();
            transaction.Rollback();                    
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace BancoDeDadosADONET
{
    internal class Program
    {
        private static string connString;
        private static DbProviderFactory factory;
        
        public static void Main(string[] args)
        {

            factory = DbProviderFactories.GetFactory(ConfigurationManager.AppSettings["dbProvider"]);
            connString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;

            using (DbConnection conn = factory.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

//                Produto p = new Produto
//                {
//                    Id = 1,
//                    Nome = "Cadeira",
//                    Valor = 1200,
//                    Descricao = "Cadeira de escritório"
//                };
                
                //Update(conn, p);
                Delete(conn,1);
                
                List<Produto> produtos = List(conn);
                
                foreach (Produto produto in produtos)
                {
                    Console.WriteLine(produto);
                }

                int qtd = Count(conn);
                Console.WriteLine($"{qtd.ToString()} iten(s) na tabela produto");

//                Produto p = Find(conn,1);
//                Console.WriteLine(p);
            }
        }
    
        static List<Produto> List(DbConnection conn)
        {
            List<Produto> produtos = new List<Produto>();
            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Id, Nome, Valor, Descricao FROM Produto ORDER BY Id";
                
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Produto produto = new Produto();
    
                        produto.Id = (int)dr["Id"];
                        produto.Nome = (string)dr["Nome"];
                        produto.Valor = Convert.ToDouble((decimal)dr["Valor"]);
                        produto.Descricao = (string)dr["Descricao"];
                        
                        produtos.Add(produto);
                    }
                }
            }
            return produtos;
        }
        
        static Produto Find(DbConnection conn, int Id)
        {
            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Id, Nome, Valor, Descricao FROM Produto WHERE Id = @Id";
                
                DbParameter param = factory.CreateParameter();
                param.ParameterName = "@Id";
                param.Value = Id;
                cmd.Parameters.Add(param);
                
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read())
                    {
                        return null;
                    }
                    
                    Produto produto = new Produto();
    
                    produto.Id = (int)dr["Id"];
                    produto.Nome = (string)dr["Nome"];
                    produto.Valor = Convert.ToDouble((decimal)dr["Valor"]);
                    produto.Descricao = (string)dr["Descricao"];

                    return produto;
                }
            }
        }

        static void Insert(DbConnection conn, Produto p)
        {
            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Produto (Id, Nome, Valor, Descricao) VALUES (@Id, @Nome, @Valor, @Descricao)";

                DbParameter param = factory.CreateParameter();
                param.ParameterName = "@Id";
                param.Value = p.Id;
                cmd.Parameters.Add(param);
                
                param = factory.CreateParameter();
                param.ParameterName = "@Nome";
                param.Value = p.Nome;
                cmd.Parameters.Add(param);
                
                param = factory.CreateParameter();
                param.ParameterName = "@Valor";
                param.Value = p.Valor;
                cmd.Parameters.Add(param);
                
                param = factory.CreateParameter();
                param.ParameterName = "@Descricao";
                param.Value = p.Descricao;
                cmd.Parameters.Add(param);

                int n = cmd.ExecuteNonQuery();

                Console.WriteLine($"Foram inseridos {n.ToString()} dados ao banco de dados");
            }
        }
        
        static void Update(DbConnection conn, Produto p)
        {
            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE Produto SET Nome = @Nome, Valor = @Valor, Descricao = @Descricao WHERE Id = @Id";

                DbParameter param = factory.CreateParameter();
                param.ParameterName = "@Id";
                param.Value = p.Id;
                cmd.Parameters.Add(param);
                
                param = factory.CreateParameter();
                param.ParameterName = "@Nome";
                param.Value = p.Nome;
                cmd.Parameters.Add(param);
                
                param = factory.CreateParameter();
                param.ParameterName = "@Valor";
                param.Value = p.Valor;
                cmd.Parameters.Add(param);
                
                param = factory.CreateParameter();
                param.ParameterName = "@Descricao";
                param.Value = p.Descricao;
                cmd.Parameters.Add(param);

                int n = cmd.ExecuteNonQuery();

                Console.WriteLine($"Foram inseridos {n.ToString()} dados ao banco de dados");
            }
        }

        static int Count(DbConnection conn)
        {
            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT COUNT(*) FROM Produto";

                int count = (int)cmd.ExecuteScalar();

                return count;
            }
            
        }
        
        static void Delete(DbConnection conn, int Id)
        {
            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM Produto WHERE Id = @Id";

                DbParameter param = factory.CreateParameter();
                param.ParameterName = "@Id";
                param.Value = Id;
                cmd.Parameters.Add(param);

                int n = cmd.ExecuteNonQuery();
            }
        }
    }

    class Produto
    {
        public int  Id { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        
        public override string ToString()
        {
            return string.Format("{0,-3}{1,-15}{2,-15:C}{3}",Id,Nome,Valor,Descricao);
        }
    }
}