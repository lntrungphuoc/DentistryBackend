namespace AppDentistry.Models.Request.User
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public List<string> Roles { get; set; }
    }
}
