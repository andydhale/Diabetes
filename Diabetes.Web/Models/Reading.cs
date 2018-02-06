using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Web.Models
{
    public class Reading
    {
        public int Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Time { get; set; }

        public double Value { get; set; }

        public ReadingTime ReadingTime { get; set; }
    }

    public enum ReadingTime
    {
        Morning,
        Lunch,
        Dinner,
        Night
    }
}
