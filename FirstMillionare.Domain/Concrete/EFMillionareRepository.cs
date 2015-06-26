using System;
using System.Collections.Generic;
using System.Linq;
using FirstMillionare.Domain.Abstract;
using FirstMillionare.Domain.Entities;

namespace FirstMillionare.Domain.Concrete
{
    public class EFMillionareRepository : IMillionareRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<Answer> Answers { get { return _context.Answers; } }
        public IQueryable<Question> Questions { get { return _context.Questions; } }
        public IQueryable<Option> Options { get { return _context.Options; } }

        public List<QuestionItem> GetQuestionsOnLevel(int level)
        {
            return (from question in Questions
                    where question.Complexity == level
                    join option in Options on question.Id equals option.QuestionId into optionsGroup
                    select new QuestionItem { Options = optionsGroup.ToList(), QuestionText = question.QuestionText, Answer = optionsGroup.FirstOrDefault(p => Answers.Count(a => a.OptionId == p.Id) == 1).OptionText }).ToList();
        }
    }
}