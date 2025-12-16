namespace InterviewPass.WebApi.Models.ResponseResult
{
    public abstract class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
