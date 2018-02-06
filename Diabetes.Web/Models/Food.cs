using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Web.Models
{
    public class Food
    {
        public int Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Time { get; set; }

        public string Description { get; set; }
    }
}
