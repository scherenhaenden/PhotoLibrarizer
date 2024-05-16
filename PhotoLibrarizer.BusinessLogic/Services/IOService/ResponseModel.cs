namespace PhotoLibrarizer.BusinessLogic.Services.IOService
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    
        public Exception? Exception { get; set; }
    }
}