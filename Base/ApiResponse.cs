public class ApiResponse<T>
{
    public T Data { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }

    public ApiResponse(bool isSuccess, string message, T data)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }

    public static ApiResponse<T> SuccessResult(T data, string message = null)
    {
        return new ApiResponse<T>(true, message, data);
    }

    public static ApiResponse<T> ErrorResult(string message)
    {
        return new ApiResponse<T>(false, message, default);
    }
}