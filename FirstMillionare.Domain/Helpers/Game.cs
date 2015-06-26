using System;
using System.Collections.Generic;
using System.Linq;
using FirstMillionare.Domain.Abstract;
using FirstMillionare.Domain.Entities;

namespace FirstMillionare.Domain.Helpers
{
    public class Game
    {
        #region Consts
        public const int COUNT_OF_QUESTION_ON_LEVEL = 5;
        public const int COUNT_OF_LEVELS = 3;
        public const int COUNT_OF_LIVES = 1;
        #endregion

        #region Fields
        private IMillionareRepository _repository;
        private List<QuestionItem> _questions;
        #endregion

        #region Properties
        public int Level { get; private set; }
        public int CurrentQuestion { get; private set; }
        public int Lives { get; set; }
        public int[] WinningSums { get; private set; }
        #endregion

        #region Constructors
        public Game(IMillionareRepository repo)
        {
            _repository = repo;

            Level = 1;
            CurrentQuestion = 0;
            Lives = COUNT_OF_LIVES;
            WinningSums = new int[] { 3000000, 1500000, 800000, 400000, 200000, 100000, 50000, 
                                            25000, 15000, 10000, 5000, 3000, 2000, 1000, 500 };
            SetQuestionsFromLevel(Level);
        }
        #endregion

        #region Methods     
        public QuestionItem GetQuestion()
        {
            if (CanChangeLevel())
            {
                SetQuestionsFromLevel(++Level);
            }

            return _questions.ElementAt(CurrentQuestion++ % COUNT_OF_QUESTION_ON_LEVEL);
        }

        private void SetQuestionsFromLevel(int level)
        {
            _questions = RandomizeQuestions(_repository.GetQuestionsOnLevel(level));            
        }

        private bool CanChangeLevel()
        {
            return ((CurrentQuestion + 1) > Level * COUNT_OF_QUESTION_ON_LEVEL) && ((Level + 1) <= COUNT_OF_LEVELS);
        }
        #endregion

        #region Helpers
        private List<QuestionItem> RandomizeQuestions(List<QuestionItem> questions)
        {
            Random random = new Random();
            for (int i = 0; i < questions.Count; i++)
            {
                int number = random.Next(0, questions.Count);
                QuestionItem temp = questions[number];
                questions.RemoveAt(number);
                questions.Insert(0, temp);
            }
            return questions.Take(5).ToList();
        }
        #endregion
    }
}
