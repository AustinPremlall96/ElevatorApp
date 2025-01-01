using AustinDVTElevator.Business.Interface;
using AustinDVTElevator.DomainModels.Entities;
using AustinDVTElevator.DomainModels.ExceptionHandlers;

namespace AustinDVTElevator.Business.Service
{
    public class ElevatorService
    {
        private readonly ILoggerService _logger;
        private readonly List<ElevatorBase> _elevators;
        private readonly int _maxFloors;

        public ElevatorService(ILoggerService logger, List<ElevatorBase> elevators, int maxFloors)
        {
            _logger = logger;
            _elevators = elevators;
            _maxFloors = maxFloors;
        }

        public async Task CallElevatorAsync(int floor, int passengers)
        {
            try
            {
                var closestElevator = _elevators
                    .Where(e => e.Status != ElevatorStatus.Moving)
                    .OrderBy(e => Math.Abs(e.CurrentFloor - floor))
                    .FirstOrDefault(e => e.CanAcceptPassengers(passengers));

                if (closestElevator == null)
                {
                    _logger.Log("Currently, no appropriate elevator is available.");
                    return;
                }

                closestElevator.PassengerCount += passengers;
                await closestElevator.MoveToFloorAsync(floor, _maxFloors);
                closestElevator.PassengerCount -= passengers;

                _logger.Log($"Elevator {closestElevator.Id} completed the request at floor {floor}.");
            }
            catch (InvalidFloorException ex)
            {
                _logger.Log($"Error: {ex.Message}");
            }
            catch (ElevatorOverloadException ex)
            {
                _logger.Log($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.Log($"Error: {ex.Message}");
            }
        }
    }
}
