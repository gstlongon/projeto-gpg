namespace Core.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public User() { }

        public User(string id, string name, string? email, string? phoneNumber, string password, string street, string number, string city, string state, string postalcode, string country)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Street = street;
            Number = number;
            City = city;
            State = state;
            Country = country;
        }
    }
}