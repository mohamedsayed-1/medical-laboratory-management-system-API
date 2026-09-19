namespace Medical_Laboratory_Management_System.Exceptions
{
    public class AppointmentNotCancellableException : Exception
    {
        public AppointmentNotCancellableException(string message) : base(message) { }
    }
}
