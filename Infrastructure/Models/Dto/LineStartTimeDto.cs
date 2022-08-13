using Domain.Entity.TrainGovernor;

namespace Infrastructure.Models.Dto
{
    public class LineStartTimeDto
    {
        public int? Id { get; set; }
        public int LineId { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public bool IsActive { get; set; }
        public LineDto Line { get; set; }

        public LineStartTimeDto() { }

        public LineStartTime ToEntity()
        {
            return new LineStartTime()
            {
                Id = Id == -1 ? 0 : (int)Id,
                LineId = LineId,
                Hour = Hour,
                Minute = Minute,
                IsActive = IsActive
            };
        }
    }
}