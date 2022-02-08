namespace Domain.Models.Dto
{
    public class LineStartTimeDto
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public bool IsActive { get; set; }
        public LineDto Line { get; set; }

        public TimeOnly Time => new TimeOnly(Hour, Minute, 0);

        public LineStartTimeDto() { }
    }
}
