namespace ApiCadastro.Services
{
    using System.Security.Cryptography;
    using System.Text;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using ApiCadastro.Models;
    using ApiCadastro.Repositories;

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public UserService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        private string GenerateSalt() => Guid.NewGuid().ToString();

        private string CreatePasswordHash(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = $"{salt}{password}";
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public async Task<string> RegisterAsync(UserDto userDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(userDto.Email);
            if (existingUser != null)
                return "Email já está em uso.";

            var salt = GenerateSalt();
            var user = new User
            {
                Nome = userDto.Nome,
                Sobrenome = userDto.Sobrenome,
                Email = userDto.Email,
                SenhaHash = CreatePasswordHash(userDto.Senha, salt),
                Salt = salt,
                Telefone = userDto.Telefone,
                DataNascimento = userDto.DataNascimento,
                Endereco = userDto.Endereco,
                Cidade = userDto.Cidade,
                Estado = userDto.Estado,
                CEP = userDto.CEP
            };

            await _userRepository.AddAsync(user);
            return "Usuário registrado com sucesso";
        }

        public async Task<string> AuthenticateAsync(UserDto userDto)
        {
            var user = await _userRepository.GetByEmailAsync(userDto.Email);
            if (user == null || user.SenhaHash != CreatePasswordHash(userDto.Senha, user.Salt))
                return null;

            return _jwtService.GenerateToken(user);
        }

        public async Task<User> GetByIdAsync(int id) => await _userRepository.GetByIdAsync(id);

        public async Task<IEnumerable<User>> GetAllAsync() => await _userRepository.GetAllAsync();
    }

}
