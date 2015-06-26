using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMillionare.Domain.Entities
{
    public class QuestionItem
    {
        public string QuestionText { get; set; }        
        public string Answer { get; set; }
        public List<Option> Options { get; set; }
    }
}
