using AustinDVTElevator.DomainModels.ExceptionHandlers;

namespace AustinDVTElevator.DomainModels.Entities
{
    public abstract class ElevatorBase
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public int Capacity { get; set; }
        public int PassengerCount { get; set; }
        public Direction Direction { get; set; }
        public ElevatorStatus Status { get; set; }

        protected ElevatorBase(int id, int capacity)
        {
            Id = id;
            Capacity = capacity;
            CurrentFloor = 0;
            Direction = Direction.Stationary;
            Status = ElevatorStatus.Stationary;
        }

        public virtual async Task MoveToFloorAsync(int targetFloor, int maxFloors)
        {
            if (targetFloor < 0 || targetFloor >= maxFloors)
                throw new InvalidFloorException($"Floor {targetFloor} is out of bounds for this building.");

            int distance = Math.Abs(CurrentFloor - targetFloor);
            Direction = targetFloor > CurrentFloor ? Direction.Up : Direction.Down;
            Status = ElevatorStatus.Moving;

            for (int i = 0; i <= distance; i++)
            {
                await Task.Delay(500);
                Console.Clear();
                Console.WriteLine($"Elevator {Id} moving {Direction}...");
                Console.WriteLine($"[{new string('=', i)}>{new string(' ', distance - i)}] Floor: {CurrentFloor + (Direction == Direction.Up ? i : -i)}");
            }

            CurrentFloor = targetFloor;
            Direction = Direction.Stationary;
            Status = ElevatorStatus.Stationary;
        }

        public virtual bool CanAcceptPassengers(int passengers)
        {
            if (PassengerCount + passengers > Capacity)
                throw new ElevatorOverloadException($"Elevator {Id} is overloaded. Capacity: {Capacity}, Requested: {PassengerCount + passengers}.");
            return true;
        }
    }
}
