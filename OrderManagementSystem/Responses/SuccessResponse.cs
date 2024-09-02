namespace OrderManagementSystem.Responses
{
    
    public class SuccessResponse<T> : BaseResponse
    {
        public T Data { get; set; }

    }
}
