namespace FaziSimpleSavings.Application.Common.Exceptions
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> Fail(string message, int statusCode, List<string>? errors = null)
            => new()
            {
                Success = false,
                Message = message,
                StatusCode = statusCode,
                Errors = errors,
                Data = default
            };

        public static ApiResponse<T> Ok(T data, string message = "Success", int statusCode = 200)
            => new()
            {
                Success = true,
                Message = message,
                StatusCode = statusCode,
                Data = data
            };
    }

}
