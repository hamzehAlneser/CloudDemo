using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudDemo.Models
{
    public class ExamModel
    {
        public int id { get; set; }
        public List<QuestionModel> questions { get; set; }
        public string examinerId { get; set; }
        public string examineId { get; set; }
        public string creationDate { get; set; }
        public string type { get; set; }
    }

}
