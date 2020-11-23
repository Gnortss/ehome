using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.ViewModels
{
    public class ApplicationUserVM
    {
        public string Id { get; set; }
        [Display(Name="Uporabniško ime")]
        public string UserName { get; set; }
        [Display(Name="Epošta")]
        public string Email { get; set; }
        [Display(Name="Telefon")]
        public string PhoneNumber { get; set; }

        [Display(Name="Slika")]
        public string ImageLink { get; set; }

        [Display(Name="Št. oglasov")]
        public int ListingsCount { get; set; }
        public List<web.Models.Listing> Listings { get; set; }
    }
}