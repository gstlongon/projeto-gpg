using Cadastro.Data;
using Cadastro.Models;

public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Address GetAddressById(int id)
    {
        return _context.Addresses.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Address> GetAddressesByUserId(int userId)
    {
        return _context.Addresses.Where(a => a.UserId == userId).ToList();
    }

    public void AddAddress(Address address)
    {
        _context.Addresses.Add(address);
        _context.SaveChanges();
    }

    public void UpdateAddress(Address address)
    {
        _context.Addresses.Update(address);
        _context.SaveChanges();
    }

    public void DeleteAddress(int id)
    {
        var address = _context.Addresses.Find(id);
        if (address != null)
        {
            _context.Addresses.Remove(address);
            _context.SaveChanges();
        }
    }
}