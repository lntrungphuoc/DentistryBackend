namespace AppDentistry.Models.Response.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
