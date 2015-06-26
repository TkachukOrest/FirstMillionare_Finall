using System.Collections.Generic;
using System.Linq;
using FirstMillionare.Domain.Entities;

namespace FirstMillionare.Domain.Abstract
{
    public interface IMillionareRepository
    {
        IQueryable<Answer> Answers { get; }
        IQueryable<Question> Questions { get; }
        IQueryable<Option> Options { get; }
        List<QuestionItem> GetQuestionsOnLevel(int level);        
    }
}
