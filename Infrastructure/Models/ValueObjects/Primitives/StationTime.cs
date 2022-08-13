namespace Infrastructure.Models.ValueObjects.Primitives
{
    public class StationTime
    {
        public int StationId { get; set; }
        public string StationName { get; set; }
        public string ArrivalTimeString { get; set; }
        public string DepartureTimeString { get; set; }

        public int ArrivalTimeInMinutes { get; set; }
        public int DepartureTimeInMinutes{ get; set; }

        public StationTime(int hour, int minute, int breakTime = 0)
        {
            ArrivalTimeInMinutes = hour * 60 + minute;
            DepartureTimeInMinutes = ArrivalTimeInMinutes + breakTime;

            ArrivalTimeString = GenerateTimeStrings(ArrivalTimeInMinutes);
            DepartureTimeString = GenerateTimeStrings(DepartureTimeInMinutes);
        }

        public StationTime(int timeInMinutes, int breakTime = 0)
        {
            ArrivalTimeInMinutes = timeInMinutes;
            DepartureTimeInMinutes = ArrivalTimeInMinutes + breakTime;

            ArrivalTimeString = GenerateTimeStrings(ArrivalTimeInMinutes);
            DepartureTimeString = GenerateTimeStrings(DepartureTimeInMinutes);
        }

        private string GenerateTimeStrings(int input)
        {
            var hourStr = (input / 60).ToString();
            if (hourStr.Length == 1)
            {
                hourStr = "0" + hourStr;
            }

            var minuteStr = (input % 60).ToString();
            if (minuteStr.Length == 1)
            {
                minuteStr = "0" + minuteStr;
            }

            return $"{hourStr}:{minuteStr}";
        }
    }
}