using Diabetes.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Web.ViewModels
{
    public class ModifyReadingViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public DateTimeOffset Time { get; set; }

        [Required]
        public ReadingTime ReadingTime { get; set; }
    }
}
