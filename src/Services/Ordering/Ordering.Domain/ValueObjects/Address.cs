namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; }= string.Empty;
    public string Email { get; set; }= string.Empty;
    public string AddressLine { get; set; }= string.Empty;
    public string City { get; set; }= string.Empty;
    public string State { get; set; }= string.Empty;
    public string Country { get; set; }= string.Empty;
    public string ZipCode { get; set; }= string.Empty;

    private Address(string firstName, string lastName, string email, string addressLine,
        string city, string state, string country, string zipcode)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AddressLine = addressLine;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipcode;
    }

    public static Address Of(string firstName, string lastName, string email, string addressLine,
        string city, string state, string country, string zipcode)
    {
        ArgumentException.ThrowIfNullOrEmpty(email);
        ArgumentException.ThrowIfNullOrEmpty(addressLine);
        ArgumentException.ThrowIfNullOrEmpty(city);
        ArgumentException.ThrowIfNullOrEmpty(state);
        ArgumentException.ThrowIfNullOrEmpty(country);
        ArgumentException.ThrowIfNullOrEmpty(zipcode);
        return new Address(firstName, lastName, email, addressLine, city, state, country, zipcode);
    }
    
}