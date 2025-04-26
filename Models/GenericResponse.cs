namespace GestorViajes.Models
{
    public class GenericResponse<T>
    {
        public T? Data { get; set; }
        public ErrorResponse? Error { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Success
        {
            get
            {
                return Error == null;
            }
        }
    }
}
