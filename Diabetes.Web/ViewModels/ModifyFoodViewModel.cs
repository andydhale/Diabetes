using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Web.ViewModels
{
    public class ModifyFoodViewModel
    {
        [Required]
        //[GreaterThan]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTimeOffset Time { get; set; }
    }
}
