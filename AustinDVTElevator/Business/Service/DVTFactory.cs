using AustinDVTElevator.DomainModels.Entities;

namespace AustinDVTElevator.Business.Service
{
    public static class ElevatorFactory
    {
        public static ElevatorBase CreateElevator(string type, int id, int capacity)
        {
            return type.ToLower() switch
            {
                "passenger" => new PassengerElevator(id, capacity),
                "highspeed" => new HighSpeedElevator(id, capacity),
                "freight" => new FreightElevator(id, capacity),
                _ => throw new ArgumentException("Invalid elevator.")
            };
        }
    }
}
