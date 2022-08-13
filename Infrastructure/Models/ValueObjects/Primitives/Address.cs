using Infrastructure.Extensions;

namespace Infrastructure.Models.ValueObjects.Primitives
{
    public class Address
    {
        public string ZipCode { get; set; }
        public int CityId { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }

        public Address(string zipCode, int cityId, string street, string streetNumber)
        {
            ZipCode = zipCode;
            CityId = cityId;
            StreetName = street;
            StreetNumber = streetNumber;
        }

        public Address(string zipCode, int cityId, string street, int streetNumber)
        {
            ZipCode = zipCode;
            CityId = cityId;
            StreetName = street;
            StreetNumber = streetNumber.ToString();
        }

        public Address(string zipCode, int cityId, string streetAddress)
        {
            ZipCode=zipCode;
            CityId=cityId;

            var decomposedAddress = streetAddress.Decompose();
            StreetName = decomposedAddress[0];
            StreetNumber = decomposedAddress[1];
        }

        public Address()
        {

        }

        public string FullAddress
        {
            get
            {
                return StreetAddress + ", " + ZipCode; //TO DO: find a way to map city name to this address
            }
        }

        public string StreetAddress
        {
            get
            {
                return StreetName + ", " + StreetNumber;
            }
        }
    }
}