using System;
using System.Data.Common;
using Db;

namespace EstruturaGenericaDeAcessoADados
{
    internal class  Program
    {
        public static void Main(string[] args)
        {
            ProdutoDAO dao = DaoFactory.CreateDao<ProdutoDAO>();
            
//            Produto p = new Produto
//            {
//                Id = 1,
//                Nome = "Televisão",
//                Valor = 1600.00,
//                Descricao = "Televisão de LED"
//            };
//            
//            dao.Insert(p);

            
            var resp = dao.Carregar(1);
            Console.WriteLine(resp);
        }
    }

    class ProdutoDAO : DAO
    {
        public void Insert(Produto p)
        {
            using (DbConnection conn = CreateConnection())
            {
                using (DbCommand cmd = CreateCommand(conn,"INSERT INTO Produto (Id,Nome,Descricao,Valor) VALUES (@Id,@Nome,@Descricao,@Valor)"))
                {
                    conn.Open(); 
                    
                    CreateParameter(cmd, "@Id", p.Id, null);
                    CreateParameter(cmd, "@Nome", p.Nome, null);
                    CreateParameter(cmd, "@Valor", p.Valor, null);
                    CreateParameter(cmd, "@Descricao", p.Descricao, null);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Produto Carregar(int Id)
        {
            using (DbConnection conn = CreateConnection())
            {
                conn.Open();
                using (DbCommand cmd = CreateCommand(conn,"SELECT Id, Nome, Descricao, Valor FROM Produto WHERE Id = @Id"))
                {
                    CreateParameter(cmd, "@Id", Id, null);
                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read())
                        {
                            return null;
                        }
                        return new Produto
                        {
                            Id = (int)dr["Id"],
                            Nome = dr["Nome"].ToString(),
                            Descricao = dr["Descricao"].ToString(),
                            Valor = Convert.ToDouble((decimal)dr["Valor"])
                        };
                    }
                }
            }
        }
        
        
        
    }

    class Produto
    {
        public int Id { get; set; }
        public string Nome{ get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}",Id.ToString(),Nome);
        }
    }
}