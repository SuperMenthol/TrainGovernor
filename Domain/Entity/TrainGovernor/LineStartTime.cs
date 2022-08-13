namespace Domain.Entity.TrainGovernor
{
    public class LineStartTime
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public bool IsActive { get; set; }

        public Line Line { get; set; }
    }
}