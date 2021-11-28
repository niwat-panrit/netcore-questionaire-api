using System;
using System.Collections.Generic;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class QuestionDataStore : DataStoreBase
    {
        // TODO: Optimize parameters

        public QuestionDataStore(IDataStoreConfig config)
            : base(config)
        {
        }

        public IEnumerable<Question> GetQuestions(int questionnaireID)
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<Question>()
                    .Where(q => q.QuestionnaireID == questionnaireID)
                    .OrderBy(q => q.DisplayOrder).Asc
                    .List();
        }

        public Question GetQuestion(int questionID)
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<Question>()
                    .Where(q => q.ID == questionID)
                    .Take(1).SingleOrDefault();
        }

        public Question Create(Question question)
        {
            using (var dbSession = OpenStatelessSession())
            {
                question.CreatedAt =
                    question.UpdatedAt =
                        DateTime.Now;
                question.ID = (int)dbSession.Insert(question);

                return question;
            }
        }

        public bool Update(Question question)
        {
            using (var dbSession = OpenSession())
            {
                question.UpdatedAt = DateTime.Now;
                dbSession.Update(question);

                return true;
            }
        }

        public bool Delete(Question question)
        {
            using (var dbSession = OpenSession())
            {
                dbSession.Delete(question);

                return true;
            }
        }
    }
}
