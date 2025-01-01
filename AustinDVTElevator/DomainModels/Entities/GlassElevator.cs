using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustinDVTElevator.DomainModels.Entities
{
    public class GlassElevator : ElevatorBase
    {
        public GlassElevator(int id, int capacity) : base(id, capacity) { }

        public override async Task MoveToFloorAsync(int targetFloor, int maxFloors)
        {
            // future use.
        }
    }
}
