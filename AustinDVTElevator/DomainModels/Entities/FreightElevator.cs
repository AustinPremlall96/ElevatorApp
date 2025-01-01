using AustinDVTElevator.DomainModels.ExceptionHandlers;

namespace AustinDVTElevator.DomainModels.Entities
{
    public class FreightElevator : ElevatorBase
    {
        public FreightElevator(int id, int capacity) : base(id, capacity) { }

        public override bool CanAcceptPassengers(int passengers)
        {
            if (passengers > 0)
                throw new ElevatorOverloadException("Freight elevators cannot carry passengers.");
            return true;
        }

        public override async Task MoveToFloorAsync(int targetFloor, int maxFloors)
        {
            await base.MoveToFloorAsync(targetFloor, maxFloors);
            Console.WriteLine($"Freight elevator {Id} arrived at floor {targetFloor}.");
        }
    }
}

