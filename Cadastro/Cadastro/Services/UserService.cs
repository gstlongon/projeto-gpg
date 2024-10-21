public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetUserById(int id)
    {
        return _userRepository.GetUserById(id);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public void CreateUser(User user)
    {
        _userRepository.AddUser(user);
    }

    public void UpdateUser(User user)
    {
        _userRepository.UpdateUser(user);
    }

    public void DeleteUser(int id)
    {
        _userRepository.DeleteUser(id);
    }

    using System.Linq;

public class UserService : IUserService
{
    private readonly List<User> _users; 

    public UserService()
    {
      
        _users = new List<User>
        {
            new User { Id = 1, Username = "john", Email = "john@example.com", Password = "1234", Role = "Admin" },
            new User { Id = 2, Username = "jane", Email = "jane@example.com", Password = "abcd", Role = "User" }
        };
    }

    
    public User ValidateUser(string usernameOrEmail, string password)
    {
        var user = _users.FirstOrDefault(u => (u.Username == usernameOrEmail || u.Email == usernameOrEmail) && u.Password == password);
        return user; 
    }
}
}