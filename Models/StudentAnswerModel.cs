using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudDemo.Models
{
    public class StudentAnswerModel
    {
        public int questionId { get; set; }
        public string answer { get; set; }
        public int examId { get; set; }
    }
}
