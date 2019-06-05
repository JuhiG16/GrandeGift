using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;
using GrandeGift.Utility;

namespace GrandeGift.ViewModels
{
    public class HamperIndexViewModel
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Details { get; set; }
        //public double Price { get; set; }
        //public string CategoryName { get; set; }
        //public bool IsContinue { get; set; }
        //public Status Status { get; set; }
        //public Category Category { get; set; }
        //public string PhotoPath{ get; set; }
        public List<Hamper> Hampers { get; set; }

        public SearchViewModel SearchModel { get; set; }
      }

    public class SearchViewModel
    {
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? searchCatId { get; set; }
    }
}
