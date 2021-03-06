﻿using Diabetes.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Web.ViewModels
{
    public class CreateInjectionViewModel
    {
        [Required]
        public int Amount { get; set; }

        [Required]
        public DateTimeOffset Time { get; set; }

        [Required]
        public InsulinType Type { get; set; }
    }
}
