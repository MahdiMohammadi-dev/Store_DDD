namespace Store.Api.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int? ItemsCount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
