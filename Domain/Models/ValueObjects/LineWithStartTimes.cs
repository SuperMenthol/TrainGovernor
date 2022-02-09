using Domain.Models.ValueObjects.Primitives;

namespace Domain.Models.ValueObjects
{
    public class LineWithStartTimes
    {
        public int LineId { get; set; }
        public List<StationTime> Collection { get; set; }

        public LineWithStartTimes() { }
    }
}