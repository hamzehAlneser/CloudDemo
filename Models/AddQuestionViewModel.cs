using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudDemo.Models
{
    public class AddQuestionViewModel
    {
        public string type { get; set; }
        public string externalId { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
