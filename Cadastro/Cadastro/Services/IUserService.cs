public interface IUserService
{
    User ValidateUser(string Email, string password);
    User GetUserById(int id);
    IEnumerable<User> GetAllUsers();
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int id);
}

