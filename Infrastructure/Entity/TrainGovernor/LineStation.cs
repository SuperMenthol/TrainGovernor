namespace Infrastructure.Entity.TrainGovernor
{
    public class LineStation
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int StationId { get; set; }
        public int NeighbourRelationId { get; set; }
        public int StationOrder { get; set; }
        public int BreakInMinutes { get; set; }
        public float AvgSpeed { get; set; }
        public bool IsActive { get; set; }

        public Line Line { get; set; }
        public TrainStation TrainStation { get; set; }
        public NeighbouringTrainStation NeighbouringTrainStation { get; set; }
    }
}