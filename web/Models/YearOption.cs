using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class YearOption
    {
        [Key]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string CodeName { get; set; }
    }
}