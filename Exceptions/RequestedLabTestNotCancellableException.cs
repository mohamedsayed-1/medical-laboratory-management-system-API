namespace Medical_Laboratory_Management_System.Exceptions
{
    public class RequestedLabTestNotCancellableException : Exception
    {
        public RequestedLabTestNotCancellableException(string message) : base(message) { }
    }
}
