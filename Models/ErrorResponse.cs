namespace GestorViajes.Models
{
    public class ErrorResponse
    {
        public string Status { get; set; } = "ok";
        public string Message { get; set; } = String.Empty;
        public string ExceptionType { get; set; } = String.Empty;
        public ErrorResponse()
        {
        }

        public ErrorResponse(Exception ex)
        {
            Status = "error";
            ExceptionType = ex.GetType().FullName ?? string.Empty;

            Message = ex.GetBaseException().Message;
        }


        public ErrorResponse(string message)
        {
            Status = "error";
            Message = message;
            ExceptionType = String.Empty;
        }
        public ErrorResponse(string message, string status)
        {
            Status = status;
            Message = message;
            ExceptionType = String.Empty;
        }
    }
}

