namespace Infrastructure.Entity.TrainGovernor
{
    public class TrainStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ZipCode { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public City City { get; set; }
    }
}