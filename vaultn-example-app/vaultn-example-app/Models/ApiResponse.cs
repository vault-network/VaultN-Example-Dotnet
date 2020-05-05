namespace vaultn_example_app.Models
{
    public class ApiResponse<T>: BaseResponse where T: class
    {
        public T Result { get; set; }
    }
}