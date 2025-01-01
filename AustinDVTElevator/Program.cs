using AustinDVTElevator.Business.Interface;
using AustinDVTElevator.Business.Service;
using AustinDVTElevator.DomainModels.Entities;
using AustinDVTElevator.Logger;

namespace DVTElevatorChallenge.Presentation
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            ILoggerService logger = new ConsoleLoggerService();
            var elevators = new List<ElevatorBase>
            {
                new PassengerElevator(1, 5),
                new HighSpeedElevator(2, 8),
                new FreightElevator(3, 15)
            };

            var elevatorService = new ElevatorService(logger, elevators, maxFloors: 10);

            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to the Elevator System");
                    Console.WriteLine("1. Call Elevator");
                    Console.WriteLine("2. Add New Elevator");
                    Console.WriteLine("3. Exit");
                    Console.Write("Choose an option: ");
                    var option = Console.ReadLine();

                    if (option == "3") break;

                    switch (option)
                    {
                        case "1":
                            Console.Write("Enter the floor number (0-9): ");
                            int floor = int.Parse(Console.ReadLine() ?? "0");

                            Console.Write("Enter the number of passengers: ");
                            int passengers = int.Parse(Console.ReadLine() ?? "0");

                            await elevatorService.CallElevatorAsync(floor, passengers);
                            break;

                        case "2":
                            Console.Write("Enter elevator type (passenger, highspeed, freight): ");
                            string type = Console.ReadLine()?.ToLower();

                            Console.Write("Enter elevator ID: ");
                            int id = int.Parse(Console.ReadLine() ?? "0");

                            Console.Write("Enter elevator capacity: ");
                            int capacity = int.Parse(Console.ReadLine() ?? "0");

                            ElevatorBase newElevator = type switch
                            {
                                "passenger" => new PassengerElevator(id, capacity),
                                "highspeed" => new HighSpeedElevator(id, capacity),
                                "freight" => new FreightElevator(id, capacity),
                                _ => throw new ArgumentException("Invalid elevator type.")
                            };

                            elevators.Add(newElevator);
                            logger.Log($"Added {type} elevator with ID {id}.");
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                catch (FormatException)
                {
                    logger.Log("Invalid input. Please enter a valid number.");
                }
                catch (ArgumentException ex)
                {
                    logger.Log($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    logger.Log($"Unexpected error: {ex.Message}");
                }
            }
        }
    }
}
