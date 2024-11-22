namespace ApiCadastro.Models
{
    using System;

    public class UserUpdateDto
    {
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Endereco { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? CEP { get; set; }
        public string? FotoBase64 { get; set; } 
    }
}
