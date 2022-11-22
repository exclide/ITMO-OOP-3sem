namespace Banks.Models;

public class ClientAddress
{
    public ClientAddress(string city, string streetName, int postalCode, int houseNumber, int apartmentNumber)
    {
        City = city;
        StreetName = streetName;
        PostalCode = postalCode;
        HouseNumber = houseNumber;
        ApartmentNumber = apartmentNumber;
    }

    public string City { get; }
    public string StreetName { get; }
    public int PostalCode { get; }
    public int HouseNumber { get; }
    public int ApartmentNumber { get; }
}