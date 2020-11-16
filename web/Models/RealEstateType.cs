using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class RealEstateType
    {
        [Key]
        public string Type { get; set; }
    }
}