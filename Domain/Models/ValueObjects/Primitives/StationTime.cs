namespace Domain.Models.ValueObjects.Primitives
{
    public class StationTime
    {
        public int StationId { get; set; }
        public string StationName { get; set; }
        public TimeOnly ArrivalTime { get; set; }
        public TimeOnly DepartureTime { get; set; }

        public StationTime(int hour, int minute, int breakTime = 0)
        {
            ArrivalTime = new TimeOnly(hour, minute, 0);
            DepartureTime = ArrivalTime.AddMinutes(breakTime);
        }
    }
}