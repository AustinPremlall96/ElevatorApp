using AustinDVTElevator.Business.Service;
using AustinDVTElevator.DomainModels.Entities;

namespace AustinDVTTests
{
    [TestFixture]
    public class Elevator
    {
        private ElevatorService _elevatorService;
        private TestLogger _logger;

        [SetUp]
        public void Setup()
        {
            _logger = new TestLogger();
            var elevators = new List<ElevatorBase>
            {
                new PassengerElevator(1, 5),
                new HighSpeedElevator(2, 8),
                new FreightElevator(3, 15)
            };

            _elevatorService = new ElevatorService(_logger, elevators, maxFloors: 10);
        }

        [Test]
        public async Task CallElevator_ValidFloorAndPassengers_ElevatorArrives()
        {
            int floor = 3;
            int passengers = 2;
            await _elevatorService.CallElevatorAsync(floor, passengers);

            Assert.That(_logger.Messages, Does.Contain($"Elevator 1 completed the request at floor {floor}."));
        }

        [Test]
        public async Task CallElevator_InvalidFloor_ThrowsInvalidFloorException()
        {

            int invalidFloor = 11; 
            int passengers = 2;
            await _elevatorService.CallElevatorAsync(invalidFloor, passengers);

            Assert.That(_logger.Messages, Does.Contain($"Error: Floor {invalidFloor} is out of bounds for this building."));
        }

        [Test]
        public async Task CallElevator_OverloadedElevator_ThrowsElevatorOverloadException()
        {
            int floor = 2;
            int passengers = 20; 
            await _elevatorService.CallElevatorAsync(floor, passengers);

            Assert.That(_logger.Messages, Does.Contain($"Error: Elevator 1 is overloaded. Capacity: 5, Requested: {passengers}."));
        }

        [Test]
        public async Task CallElevator_FreightElevatorRejectsPassengers()
        {
            var freightElevator = new FreightElevator(3, 15);
            _elevatorService = new ElevatorService(_logger, new List<ElevatorBase> { freightElevator }, maxFloors: 10);
            await _elevatorService.CallElevatorAsync(3, 5); 

            Assert.That(_logger.Messages, Does.Contain("Error: Freight elevators cannot carry passengers."));
        }

        [Test]
        public async Task CallElevator_NoElevatorAvailable_LogsMessage()
        {
            _elevatorService = new ElevatorService(_logger, new List<ElevatorBase>(), maxFloors: 10);
            await _elevatorService.CallElevatorAsync(3, 2);

            Assert.That(_logger.Messages, Does.Contain("Currently, no appropriate elevator is available."));
        }
    }
}