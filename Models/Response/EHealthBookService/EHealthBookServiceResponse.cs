using AppDentistry.Models.Response.EHealthBook;
using AppDentistry.Models.Response.Service;

namespace AppDentistry.Models.Response.EHealthBookService
{
    public class EHealthBookServiceResponse
    {
        public int Id { get; set; }
        public int IdEHealthBook { get; set; }
        public int IdService { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public EHealthBookResponse EHealthBook { get; set; }
        public ServiceResponse Service { get; set; }
    }
}
