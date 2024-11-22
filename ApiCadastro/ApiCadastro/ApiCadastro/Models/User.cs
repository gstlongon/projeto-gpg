namespace ApiCadastro.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; } // Precisa pra usar o sha256
        public string Salt { get; set; } // Precisa pra usar o sha256
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

        public byte[]? Foto { get; set; }
        public string Role { get; set; } = "User"; 
    }
}
