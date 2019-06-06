namespace RSA.Domain.Dominio
{
    public class Texto
    {
        public string TextoASerManipulado { get; set; }
        public int Id { get; set; }
        public Tipo Tipo { get; set; }//falta mapear
        public int IdAuthor { get; set; }

        public Texto(string textoASerManipulado,Tipo? tipo,int idAuthor)
        {
            TextoASerManipulado = textoASerManipulado;
            
            Tipo = tipo != null ? 
                tipo.Value : 
                Tipo.Descriptografado;
            
            IdAuthor = idAuthor;
        }

        public override string ToString()
        {
            return string.Format("Texto {0}, Autor {1}: {2}", Id.ToString(), IdAuthor.ToString(), TextoASerManipulado);
        }
    }

    public enum Tipo
    {
        Criptografado,
        Descriptografado
    }
}