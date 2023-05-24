using AppDentistry.Models.Response.Role;

namespace AppDentistry.Interface.Services
{
    public interface IRoleServices
    {
        List<RoleResponse> GetAll();
    }
}
