using Infrastructure.Models.Dto;
using Infrastructure.Models.ValueObjects.Primitives;

namespace Infrastructure.Models.ValueObjects
{
    public class LineWithStartTimes
    {
        public int LineId { get; set; }
        public string LineName { get; set; }
        public List<IGrouping<string, StationTime>> Collection { get; set; }

        public LineWithStartTimes() { }
    }
}