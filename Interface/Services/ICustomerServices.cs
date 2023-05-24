using AppDentistry.Models.Request.Customer;
using AppDentistry.Models.Response.Customer;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Interface.Services
{
    public interface ICustomerServices
    {
        List<CustomerResponse> GetAll();
        HttpResponse Login(string PhoneNumber, string password);
        HttpResponse Create(CustomerRequest request);
        HttpResponse Update(CustomerRequest request);
        HttpResponse Delete(int id);
    }
}
