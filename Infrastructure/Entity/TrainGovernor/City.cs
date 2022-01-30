namespace Infrastructure.Entity.TrainGovernor
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ZipCode { get; set; }
        public bool IsActive { get; set; }

        public List<TrainStation>? Stations { get; set; }
    }
}