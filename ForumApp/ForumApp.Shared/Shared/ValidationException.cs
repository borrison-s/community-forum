namespace ForumApp.Shared.Shared
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
