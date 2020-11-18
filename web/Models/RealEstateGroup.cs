using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class RealEstateGroup
    {
        [Key]
        public int Id { get; set; }
        public string Group { get; set; }

        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public RealEstateType REType { get; set; }
    }
}