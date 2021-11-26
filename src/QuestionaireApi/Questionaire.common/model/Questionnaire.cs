using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual IEnumerable<Question> GetQuestions()
        {
            return QuestionDataStore.Instance
                .GetQuestions(this.ID);
        }

        public virtual IEnumerable<IEnumerable<Answer>> GetAnswers()
        {
            var answers = AnswerDataStore.Instance
                .GetAnswers(this.ID);

            // TODO:
            //  1) Use SQL join
            //  2) Order answers by question's display order

            return answers.GroupBy(a => a.SessionID);
        }

        public virtual Question GetPreviousQuestion(Question question) =>
            QuestionaireDataStore.Instance
                .GetPreviousQuestion(question);

        public virtual Question GetNextQuestion(Question question) =>
            QuestionaireDataStore.Instance
                .GetNextQuestion(question);

        public virtual int GetMinDisplayOrder() =>
            QuestionaireDataStore.Instance
                .GetMinDisplayOrder(this.ID);

        public virtual int GetMaxDisplayOrder() =>
            QuestionaireDataStore.Instance
                .GetMaxDisplayOrder(this.ID);
    }
}
