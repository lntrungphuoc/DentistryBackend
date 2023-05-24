using AppDentistry.Interface.Services;
using AppDentistry.Models.Response.Role;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AppDentistry.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleServices(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<RoleResponse> GetAll()
        {
            return _roleManager.Roles
                .Select(e => new RoleResponse()
                {
                    Name = e.Name,
                    DisplayName = e.DisplayName
                })
                .ToList();
        }
    }
}
