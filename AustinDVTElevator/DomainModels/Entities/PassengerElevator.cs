using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustinDVTElevator.DomainModels.Entities
{
    public class PassengerElevator : ElevatorBase
    {
        public PassengerElevator(int id, int capacity) : base(id, capacity) { }

        public override async Task MoveToFloorAsync(int targetFloor, int maxFloors)
        {
            await base.MoveToFloorAsync(targetFloor, maxFloors);
            Console.WriteLine($"Passenger elevator {Id} arrived at floor {targetFloor}.");
        }
    }
}
