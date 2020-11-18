using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class ListingType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}