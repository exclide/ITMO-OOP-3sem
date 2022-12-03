using Banks.Exceptions;

namespace Banks.Models;

public class ClientAddressBuilder
{
    private string _city;
    private string _streetName;
    private int _postalCode;
    private int _houseNumber;
    private int _apartmentNumber;

    public ClientAddressBuilder SetCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new BankException("city was null or whitespace");
        }

        _city = city;
        return this;
    }

    public ClientAddressBuilder SetStreetName(string streetName)
    {
        if (string.IsNullOrWhiteSpace(streetName))
        {
            throw new BankException("street name was null or whitespace");
        }

        _streetName = streetName;
        return this;
    }

    public ClientAddressBuilder SetPostalCode(int postalCode)
    {
        if (postalCode < 100000 || postalCode >= 1000000)
        {
            throw new BankException("Wrong postal code");
        }

        _postalCode = postalCode;
        return this;
    }

    public ClientAddressBuilder SetHouseNumber(int houseNumber)
    {
        if (houseNumber < 0)
        {
            throw new BankException("House number was negative");
        }

        _houseNumber = houseNumber;
        return this;
    }

    public ClientAddressBuilder SetApartmentNumber(int apartmentNumber)
    {
        if (apartmentNumber < 0)
        {
            throw new BankException("Apartment number was negative");
        }

        _apartmentNumber = apartmentNumber;
        return this;
    }

    public ClientAddress GetClientAddress()
    {
        return new ClientAddress(_city, _streetName, _postalCode, _houseNumber, _apartmentNumber);
    }
}