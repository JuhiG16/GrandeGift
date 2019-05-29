using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;
using GrandeGift.Utility;
using Microsoft.AspNetCore.Http;

namespace GrandeGift.ViewModels
{
    public class HamperDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; }
        public bool IsContinue { get; set; }
        public Status Status { get; set; }
        public string PhotoPath { get; set; }
    }
}
