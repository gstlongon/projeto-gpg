using Cadastro.Models;

public interface IAddressService
{
    Address GetAddressById(int id);
    IEnumerable<Address> GetAddressesByUserId(int userId);
    void CreateAddress(Address address);
    void UpdateAddress(Address address);
    void DeleteAddress(int id);
}