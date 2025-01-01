namespace AustinDVTElevator.DomainModels.Entities
{
    public class InvalidFloorException : Exception
    {
        public InvalidFloorException(string message) : base(message) { }
    }
}
