using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Region
    {
        public int Id { get; set; }
        [Display(Name="Regija")]
        public string Name { get; set; }
    }
}