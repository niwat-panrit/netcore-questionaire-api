using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Questionaire.common.model;
using Questionaire.common.tool;

namespace Questionaire.common.datastore
{
    public class AnswerDataStore : DataStoreBase
    {
        // TODO: Optimize parameters

        private ICache<ISession> sessionCache;

        public AnswerDataStore(IDataStoreConfig config)
            : base(config)
        {
            this.sessionCache = new DataStoreSessionCache(this);
        }

        public IEnumerable<IEnumerable<Answer>> GetAnswers(Questionnaire questionaire)
        {
            // TODO:
            //  1) Use SQL join
            //  2) Order answers by question's display order

            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<Answer>()
                    .Where(a => a.QuestionnaireID == questionaire.ID)
                    .List().GroupBy(a => a.SessionID);
        }

        public bool Submit(Session session, Question question, Answer newAnswer, bool more, List<string> warnings, out bool isException)
        {
            // TODO: Check relation of questionnaire, session and quest ids in answer

            var cacheKey = question;

            try
            {
                var dbSession = this.sessionCache.GetSession(cacheKey);

                newAnswer.CreatedAt =
                    newAnswer.UpdatedAt =
                        DateTime.Now;
                dbSession.Save(newAnswer);
                isException = AnswerExceptionDataStore.Instance
                    .IsExceptionalAnswer(question, newAnswer, dbSession);

                return true;
            }
            finally
            {
                if (!more)
                    this.sessionCache.ClearSession(cacheKey);
            }
        }
    }
}
