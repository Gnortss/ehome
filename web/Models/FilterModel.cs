
using System;
namespace web.Models {
    public class FilterModel {
        public int id_real_estate { get; set; }
        public int id_listing { get; set; }
        public string size { get; set; }
        public string region { get; set; }
        public string price { get; set; }
        public string year { get; set; }
    }
}