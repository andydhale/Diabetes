using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Web.Models
{
    public class InsulinInjection
    {
        public int Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Time { get; set; }

        public int Amount { get; set; }

        public InsulinType Type { get; set; }
    }
}
