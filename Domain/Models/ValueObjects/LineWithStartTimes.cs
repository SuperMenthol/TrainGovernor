using Domain.Models.Dto;
using Domain.Models.ValueObjects.Primitives;

namespace Domain.Models.ValueObjects
{
    public class LineWithStartTimes
    {
        public int LineId { get; set; }
        public string LineName { get; set; }
        public List<IGrouping<string, StationTime>> Collection { get; set; }

        public LineWithStartTimes() { }
    }
}