using System;
using System.Collections.Generic;
using Questionaire.common.datastore;

namespace Questionaire.common.model
{
    public class Questionnaire
    {
        public virtual int ID { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime ExpiredAt { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public Questionnaire()
        {
        }

        public IEnumerable<Question> GetQuestions()
        {
            return QuestionDataStore.Instance
                .GetQuestions(this.ID);
        }

        public IEnumerable<IEnumerable<Answer>> GetAnswers()
        {
            throw new NotImplementedException();
        }

        public Question GetPreviousQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public Question GetNextQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public int GetMinDisplayOrder()
        {
            throw new NotImplementedException();
        }

        public int GetMaxDisplayOrder()
        {
            throw new NotImplementedException();
        }
    }
}
