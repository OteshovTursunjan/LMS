namespace LMS.DTOs
{
    public class LoginResponseDTO
    {
        internal string token;

        public string Login { get; set; }
        public UserResponseDTO user { get; set; }
    }
}
