using Infrastructure.Models.ValueObjects.Primitives;

namespace Infrastructure.Models.ValueObjects
{
    public class Station
    {
        private string _name;
        private Address _address;

        private bool _isActive;
        private string? _remarks;

        public Station(string name, Address address, bool isActive = true, string? remarks = null)
        {
            _name = name;
            _address = address;
            _isActive = isActive;
            _remarks = remarks;
        }
    }
}