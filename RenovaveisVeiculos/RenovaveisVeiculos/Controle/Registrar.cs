using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RenovaveisVeiculos.Modelo;

namespace RenovaveisVeiculos.Controle
{
    [DisplayName("Controller de registros")]
    [Description("Controlador de registros do software Renovaveis veículos \n" +
                 "Responsavel por criar, inserir, atualizar e remover dados da folha de registros")]
    public class Registrar
    {
        private static string SourcePath { get; set; }
        
        [Description("Construtor do controle de registros")]
        public Registrar(string path)
        {
            SourcePath = path;
        }

        private static StreamReader SetReader() => new StreamReader(SourcePath);
        private static StreamWriter SetWriter() => new StreamWriter(SourcePath);
        private static void DeleteDocumment() => File.Delete(SourcePath);
        private static StreamWriter CreateDocumment() => File.CreateText(SourcePath);
        
        //Encontra
        [DisplayName("Encontra registros")]
        [Description("Verifica a existencia ou a não existencia de registros com aquele determinado Id")]
        public bool FindById(int idTransacao) => List().Any(x => x.Id == idTransacao);
        
        //Inserir
        [DisplayName("Insere registros")]
        [Description("Verifica se há registro com o id indicado, caso não tenha insere-se o registro e caso contrário lança uma exceção")]
        public void Insert(Transacao transacao)
        {
            try
            {
                using (var escritor = SetWriter())
                {
                    if (!FindById(transacao.Id))
                    {
                        escritor.WriteLine(transacao.ToString());
                    }
                    else
                    {
                        throw new ApplicationException("Já existe um registro com esse Id");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationException("Ocorreu um erro durande a inserção do registro");
            }
        }
        
        //Cria e insere
        [DisplayName("Cria e insere")]
        [Description("Usado para o caso de não haver o documento indicado o método cria o documento e insere os registros")]
        public async Task CreateAndInsert(Transacao transacao)
        {
            try
            {
                using (var creator = CreateDocumment())
                {
                    await creator.WriteLineAsync(transacao.ToString());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        //Alterar
        [DisplayName("Altera o registro")]
        [Description("Usado para promover a alteração do registro, caso ele exista, caso não exista o método lançará uma exceção")]
        public void Update(Transacao transacaoOld, Transacao transacao)
        {

            try
            {
                if (FindById(transacaoOld.Id))
                {
                    var listTransacao = List();//Pega a lista de todo registro
                    listTransacao.Remove(transacaoOld);//remove o registro antigo 
                    listTransacao.Add(transacao);//adiciona o registro novo
                    var translistresult = new List<Transacao>(listTransacao.OrderBy(x=>x.Id));//realoca os registros ordenadamente em uma nova lista
                
                    ReplaceDocumment(translistresult);//Remonta o documento
                }
                else
                {
                    throw new ApplicationException("Não existe o registro a ser editado \nRecomendação: Inserção do registro");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        [DisplayName("Remove registro")]
        [Description("Usado para a remoção de registro, caso ele exista, caso não exista o método lançará uma exceção")]
        //Remover
        public void Remove(Transacao transacao)
        {
            try
            {
                if (FindById(transacao.Id))
                {
                    var listTransacao = List(); //Pega a lista de todo registro
                    listTransacao.Remove(transacao); //remove o registro antigo 
                    var translistresult =
                        new List<Transacao>(listTransacao.OrderBy(x =>
                            x.Id)); //realoca os registros ordenadamente em uma nova lista

                    ReplaceDocumment(translistresult);//remonta o documento
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void ReplaceDocumment(List<Transacao> translistresult)
        {
            try
            {
                //Exclui o docummento antigo
                DeleteDocumment();
                using (var replace = CreateDocumment())//Cria o documento junto ao seu escritor
                {
                    foreach (var transacaoItem in translistresult)
                    {
                        //reescreve o documento com os novos registros ordenados
                        replace.WriteLine(transacaoItem);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        //Listar
        [DisplayName("Listagem dos registros")]
        [Description("Responsavel por promover a listagem dos registros")]
        public List<Transacao> List()
        {
            try
            {
                var transList = new List<Transacao>();
                using (var leitor = SetReader())
                {
                    while (!leitor.EndOfStream)
                    {
                        var conteudo  = leitor.ReadLineAsync().ToString().Split('\\');
                
                        var transacao = conteudo[0].Split(' ');
                        var cliente   = conteudo[1].Split(' ');
                        var conta     = conteudo[2].Split(' ');
                        var veiculo   = conteudo[3].Split(' ');
                
                        transList.Add(MontaTransacao(
                                transacao,
                                MontaCliente(
                                    cliente,
                                    MontaContaCorrente(conta)
                                ),
                                MontaVeiculo(veiculo)
                            )
                        );
                    }    
                }
                return transList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationException("Ocorreu um erro durante a listagem dos registros");
            }
        }

        private static Transacao MontaTransacao(string[] transacao,Cliente cliente, Veiculo veiculo)
        {
            try
            {
                var id         = int.Parse(transacao[0]);
                var ano        = DateTime.Parse(transacao[1]);
                var quantidade = int.Parse(transacao[2]);
            
                return new Transacao(id, ano, cliente, veiculo, quantidade);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationException("Ocorreu um erro durante a montagem da Transação");
            }
        }

        private static Cliente MontaCliente(string[] cliente, ContaCorrente conta)
        {
            try
            {
                var cpf  = cliente[0];
                var Nome = cliente[1];
            
                return new Cliente(cpf,Nome,conta);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationException("Ocorreu um erro durante a montagem do Cliente");
            }
        }

        private static ContaCorrente MontaContaCorrente(string[] conta)
        {
            try
            {
                var titular = conta[0];
                var saldo   = double.Parse(conta[1]);
                if (conta[2] is null)
                {
                    return new ContaCorrente(titular,saldo);
                }
                else
                {
                    var limite = double.Parse(conta[2]);
                    return new ContaCorrenteEspecial(titular, saldo, limite);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationException("Ocorreu um erro durante a montagem da Conta Corrente");
            }
            
        }

        private static Veiculo MontaVeiculo(string[] veiculo)
        {
            try
            {
                var id           = int.Parse(veiculo[0]);
                var marca        = veiculo[1];
                var modelo       = veiculo[2];
                var cor          = (Cor) Enum.Parse(typeof(Cor), veiculo[3]);
                var ano          = DateTime.Parse(veiculo[4]);
                var combustivel  = (Combustivel) Enum.Parse(typeof(Combustivel), veiculo[5]);
                var valor        = double.Parse(veiculo[6]);
            
                return new Veiculo(id,marca,modelo,cor,ano,combustivel,valor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationException("Ocorreu um erro durante a montagem do Veiculo");
            }
        }
    }
}