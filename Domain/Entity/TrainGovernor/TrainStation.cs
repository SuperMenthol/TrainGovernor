﻿namespace Domain.Entity.TrainGovernor
{
    public class TrainStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public City City { get; set; }
        public List<NeighbouringTrainStation> NeighbouringTrainStations { get; set; }
        public List<NeighbouringTrainStation> NeighbourTrainStations { get; set; }
        public List<LineStation> LinesOfStation { get; set; }
    }
}