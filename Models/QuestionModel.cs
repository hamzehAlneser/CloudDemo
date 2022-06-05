using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudDemo.Models
{
    public class QuestionModel
    {
        public int id { get; set; }
        public string question { get; set; }
        public string answer { get; set; } = "5";
        public string type { get; set; } = "CS";
    }
}
