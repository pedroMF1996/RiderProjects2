namespace RSA.Domain.Dominio
{
    public class User
    {
        public int  Id { get; private set; }
        public string Nome { get; private set; }
        public string ChavePrivada { get; private set; }

        public User(int id, string nome, string chavePrivada)
        {
            Id = id;
            Nome = nome;
            ChavePrivada = chavePrivada;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}, {2}",Id.ToString(),Nome,ChavePrivada);
        }
    }
}