using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class RealEstateGroup
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Vrsta - Tip")]
        public string FullName { get; set; }
        [Display(Name="Vrsta")]
        public string Group { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public RealEstateType REType { get; set; }
    }
}