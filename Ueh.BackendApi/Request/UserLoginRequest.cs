namespace Ueh.BackendApi.Request
{
    public class UserLoginRequest
    {
        public string userId { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string sdt { get; set; }
        public string role { get; set; }
    }
}
