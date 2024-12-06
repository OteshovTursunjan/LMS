namespace LMS.DTOs
{
    public class ResponseDTO<T>
    {
        public string Error { get; set; }
        public T? Data { get; set; }
    }
}
