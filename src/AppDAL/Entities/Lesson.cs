using System;
using System.Collections.Generic;
using System.Text;

namespace AppDAL.Entities
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Kind { get; set; }
    }
}
