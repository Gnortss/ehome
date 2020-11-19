using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class PriceOption
    {
        [Key]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string CodeName { get; set; }
        public int ListingType { get; set; }
        [ForeignKey("ListingType")]
        public ListingType LType { get; set; }
    }
}