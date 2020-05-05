namespace vaultn_example_app.Models
{
    public class ErrorResponse: BaseResponse
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }
}