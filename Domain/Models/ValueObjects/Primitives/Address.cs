namespace Domain.Models.ValueObjects.Primitives
{
    public class Address
    {
        private string _postCode;
        private int _cityId;
        private string _streetAddress;
        private string _streetNumber;

        public Address(string postCode, int cityId, string street, string streetNumber)
        {
            _postCode = postCode;
            _cityId = cityId;
            _streetAddress = street;
            _streetNumber = streetNumber;
        }

        public Address(string postCode, int cityId, string street, int streetNumber)
        {
            _postCode = postCode;
            _cityId = cityId;
            _streetAddress = street;
            _streetNumber = streetNumber.ToString();
        }

        public string FullAddress
        {
            get
            {
                return _streetAddress + ", " + _streetNumber + ", " + _postCode + " " + _cityId;
            }
        }

        public string StreetAddress
        {
            get
            {
                return _streetAddress + ", " + _streetNumber;
            }
        }
    }
}