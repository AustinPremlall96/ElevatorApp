namespace AustinDVTElevator.DomainModels.ExceptionHandlers
{
    public class ElevatorOverloadException : Exception
    {
        public ElevatorOverloadException(string message) : base(message) { }
    }
}
