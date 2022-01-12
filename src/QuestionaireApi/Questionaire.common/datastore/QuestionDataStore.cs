using System;
using System.Collections.Generic;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class QuestionDataStore : DataStoreBase
    {
        public QuestionDataStore(DataStoreConfig config)
            : base(config)
        {
        }

        /// <summary>
        /// Get list of questions of specified questionnaire.
        /// </summary>
        /// <param name="questionnaire">The questionnaire</param>
        /// <returns>List of questionnaire's questions</returns>
        public IList<Question> GetQuestions(Questionnaire questionnaire)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Question>()
                    .Where(q => q.QuestionnaireID == questionnaire.ID)
                    .OrderBy(q => q.DisplayOrder).Asc
                    .List();
        }

        /// <summary>
        /// Get the question by identifier.
        /// </summary>
        /// <param name="questionID">The identifier</param>
        /// <returns>The question</returns>
        public Question GetQuestion(int questionID)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Question>()
                    .Where(q => q.ID == questionID)
                    .Take(1).SingleOrDefault();
        }

        /// <summary>
        /// Save new question.
        /// </summary>
        /// <param name="question">The question</param>
        public void Create(Question question)
        {
            using (var dbSession = OpenSession())
            {
                // TODO: Use DB's on create and update
                question.CreatedAt =
                    question.UpdatedAt =
                        DateTime.Now;

                dbSession.Save(question);
            }
        }

        /// <summary>
        /// Update specified question.
        /// </summary>
        /// <param name="question">The question</param>
        public void Update(Question question)
        {
            using (var dbSession = OpenSession())
            {
                // TODO: Use DB's on update
                question.UpdatedAt = DateTime.Now;

                dbSession.Update(question);
            }
        }

        /// <summary>
        /// Delete specified question.
        /// </summary>
        /// <param name="question">The question</param>
        public void Delete(Question question)
        {
            using (var dbSession = OpenSession())
            {
                dbSession.Delete(question);
            }
        }
    }
}
