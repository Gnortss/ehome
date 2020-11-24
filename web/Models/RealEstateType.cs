using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class RealEstateType
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Tip")]
        public string Type { get; set; }
    }
}