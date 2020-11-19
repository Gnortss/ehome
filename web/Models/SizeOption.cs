using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class SizeOption
    {
        [Key]
        public int Id { get; set;}
        public string DisplayName { get; set; }
        public string CodeName { get; set; }
    }
}