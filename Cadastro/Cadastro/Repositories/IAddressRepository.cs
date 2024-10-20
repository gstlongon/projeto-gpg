using Cadastro.Models;

public interface IAddressRepository
{
    Address GetAddressById(int id);
    IEnumerable<Address> GetAddressesByUserId(int userId);
    void AddAddress(Address address);
    void UpdateAddress(Address address);
    void DeleteAddress(int id);
}