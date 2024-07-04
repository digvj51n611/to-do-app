namespace ToDoApp.Server.Authentication.Models
{
    public class AuthResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string? TokenString { get; set; }

    }
}
