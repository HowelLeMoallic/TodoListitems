namespace TodoListItems.Application.DTO
{
    public class ServiceResponse<T>
    {
        public required ServiceResponseCode Code { get; set; }
        public required string Message { get; set; }
        public bool IsSuccess => Code == ServiceResponseCode.Success;
        public T? Data { get; set; }
    }

    public enum ServiceResponseCode
    {
        Success = 200,
        Error = 900,
        ErrorDB = 500,
        ErrorBusiness = 700
    }
}
