using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.Customer;
using AppDentistry.Models.Response.Customer;
using Domain;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly AppDbContext _dbContext;

        public CustomerServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HttpResponse Create(CustomerRequest request)
        {
            if (request == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            if (_dbContext.Customers.FirstOrDefault(x => x.PhoneNumber == request.PhoneNumber) != null)
            {
                return HttpResponse.OK(message: "Số điện thoại này đã tồn tại!");
            }

            if (_dbContext.Customers.FirstOrDefault(x => x.BHYT == request.BHYT) != null)
            {
                return HttpResponse.OK(message: "Số bảo hiểm này đã tồn tại!");
            }

            var customer = new Customer
            {
                CustomerName = request.CustomerName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Password = request.Password,
                CCCD = request.CCCD,
                BHYT = request.BHYT,
                CreatedDate = DateTime.Now,
                CreatedBy = request.CreatedBy,
            };

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Thêm mới khách hàng thành công");
        }

        public HttpResponse Delete(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Xóa khách hàng thành công");
        }

        public List<CustomerResponse> GetAll()
        {
            return _dbContext.Customers.OrderByDescending(x => x.CreatedDate).Select(x => new CustomerResponse
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                Password = x.Password,
                CCCD = x.CCCD,
                BHYT = x.BHYT,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy
            }).ToList();
        }

        public HttpResponse Login(string PhoneNumber, string password)
        {
            var customer = _dbContext.Customers.FirstOrDefault(x => x.PhoneNumber == PhoneNumber && x.Password == password);

            if (customer == null)
                return HttpResponse.Error(message: "Đăng nhập không thành công, vui lòng kiểm tra lại thông tin!");

            var customerResponse = new CustomerResponse
            {
                Id = customer.Id,
                CustomerName = customer.CustomerName,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                Password = customer.Password,
                CCCD = customer.CCCD,
                BHYT = customer.BHYT,
                CreatedDate = customer.CreatedDate,
                CreatedBy = customer.CreatedBy,
                ModifiedDate = customer.ModifiedDate,
                ModifiedBy = customer.ModifiedBy
            };

            return HttpResponse.OK(message: "Đăng nhập thành công", data: customerResponse);
        }

        public HttpResponse Update(CustomerRequest request)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == request.Id);

            if (customer == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var listCustomer = _dbContext.Customers.ToList();
            listCustomer.Remove(customer);

            if (listCustomer.FirstOrDefault(x => x.PhoneNumber == request.PhoneNumber) != null)
            {
                return HttpResponse.OK(message: "Số điện thoại này đã tồn tại!");
            }

            if (listCustomer.FirstOrDefault(x => x.BHYT == request.BHYT) != null)
            {
                return HttpResponse.OK(message: "Số bảo hiểm này đã tồn tại!");
            }

            customer.Id = request.Id;
            customer.CustomerName = request.CustomerName;
            customer.PhoneNumber = request.PhoneNumber;
            customer.Address = request.Address;
            customer.Password = request.Password;
            customer.CCCD = request.CCCD;
            customer.BHYT = request.BHYT;
            customer.ModifiedDate = DateTime.Now;
            customer.ModifiedBy = request.ModifiedBy;

            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Sửa thông tin khách hàng thành công");
        }
    }
}
