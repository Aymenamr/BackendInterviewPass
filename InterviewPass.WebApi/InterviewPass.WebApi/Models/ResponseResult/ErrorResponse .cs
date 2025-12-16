namespace InterviewPass.WebApi.Models.ResponseResult
{
    public class ErrorResponse:ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }


    }
}
