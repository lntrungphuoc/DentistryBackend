using System.ComponentModel.DataAnnotations;

namespace AppDentistry.Models.Response.News
{
    public class NewsResponse
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string URL { get; set; }
        public string Thumbnail { get; set; }
        public bool ForMobile { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
