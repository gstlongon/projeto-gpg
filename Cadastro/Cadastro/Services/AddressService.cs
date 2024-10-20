using Cadastro.Models;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public Address GetAddressById(int id)
    {
        return _addressRepository.GetAddressById(id);
    }

    public IEnumerable<Address> GetAddressesByUserId(int userId)
    {
        return _addressRepository.GetAddressesByUserId(userId);
    }

    public void CreateAddress(Address address)
    {
        _addressRepository.AddAddress(address);
    }

    public void UpdateAddress(Address address)
    {
        _addressRepository.UpdateAddress(address);
    }

    public void DeleteAddress(int id)
    {
        _addressRepository.DeleteAddress(id);
    }
}