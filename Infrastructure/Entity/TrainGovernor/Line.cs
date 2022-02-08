namespace Infrastructure.Entity.TrainGovernor
{
    public class Line
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public List<LineStation> LineStations { get; set; }
        public List<LineStartTime> StartTimes { get; set; }
    }
}