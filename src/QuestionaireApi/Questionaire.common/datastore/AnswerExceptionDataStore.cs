using NHibernate;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class AnswerExceptionDataStore : DataStoreBase
    {
        // TODO: Optimize parameters

        public AnswerExceptionDataStore(DataStoreConfig config)
            : base(config)
        {
        }

        public bool IsExceptionalAnswer(Question question, Answer answer, ISession dbSession = null)
        {
            bool IsException()
            {
                var exception = dbSession.QueryOver<AnswerException>()
                    .Where(e => e.QuestionnaireID == question.QuestionnaireID &&
                                e.QuestionID == question.ID &&
                                e.Value == answer.Value)
                    .Take(1).SingleOrDefault();

                return exception != null;
            }

            bool result;
            if (dbSession == null)
            {
                try
                {
                    dbSession = OpenSession();
                    result = IsException();
                }
                finally
                {
                    dbSession?.Dispose();
                }
            }
            else
                result = IsException();

            return result;
        }
    }
}
