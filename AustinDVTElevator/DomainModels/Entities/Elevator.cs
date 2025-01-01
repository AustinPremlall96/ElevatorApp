namespace AustinDVTElevator.DomainModels.Entities
{
    public class Elevator
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public Direction Direction { get; set; }
        public int Capacity { get; set; }
        public int PassengerCount { get; set; }
        public ElevatorStatus Status { get; set; }

        public Elevator(int id, int capacity)
        {
            Id = id;
            Capacity = capacity;
            CurrentFloor = 0;
            Direction = Direction.Stationary;
            Status = ElevatorStatus.Stationary;
        }

        public bool CanAcceptPassengers(int newPassengers)
        {
            return PassengerCount + newPassengers <= Capacity;
        }
    }
}
