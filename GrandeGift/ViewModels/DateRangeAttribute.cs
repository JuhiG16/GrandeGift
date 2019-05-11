using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GrandeGift.ViewModels
{
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute(string minimum): base(typeof(DateTime), minimum, DateTime.Now.ToShortDateString())
        {

        }
    }
}
