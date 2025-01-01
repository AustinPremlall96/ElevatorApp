using AustinDVTElevator.DomainModels.ExceptionHandlers;

namespace AustinDVTElevator.DomainModels.Entities
{
    public class HighSpeedElevator : ElevatorBase
    {
        public HighSpeedElevator(int id, int capacity) : base(id, capacity) { }

        public override async Task MoveToFloorAsync(int targetFloor, int maxFloors)
        {
            if (targetFloor < 0 || targetFloor >= maxFloors)
                throw new InvalidFloorException($"Floor {targetFloor} is out of bounds for this building.");

            int distance = Math.Abs(CurrentFloor - targetFloor);
            Direction = targetFloor > CurrentFloor ? Direction.Up : Direction.Down;
            Status = ElevatorStatus.Moving;

            for (int i = 0; i <= distance; i++)
            {
                await Task.Delay(250);
                Console.Clear();
                Console.WriteLine($"High-speed elevator {Id} moving {Direction}...");
                Console.WriteLine($"[{new string('=', i)}>{new string(' ', distance - i)}] Floor: {CurrentFloor + (Direction == Direction.Up ? i : -i)}");
            }

            CurrentFloor = targetFloor;
            Direction = Direction.Stationary;
            Status = ElevatorStatus.Stationary;

            Console.WriteLine($"High-speed elevator {Id} arrived at floor {targetFloor}.");
        }
    }
}
