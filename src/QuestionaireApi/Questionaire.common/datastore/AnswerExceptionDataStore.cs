using NHibernate;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class AnswerExceptionDataStore : DataStoreBase
    {
        public AnswerExceptionDataStore(DataStoreConfig config)
            : base(config)
        {
        }

        /// <summary>
        /// Check whether a specified answer is one of the exception of specified question.
        /// </summary>
        /// <param name="answer">The answer to be checked.</param>
        /// <param name="dbSession">DB session in case of need to run query on opened session.</param>
        /// <returns></returns>
        public bool IsExceptionalAnswer(Answer answer, ISession dbSession = null)
        {
            bool IsExceptionalAnswerInternal()
            {
                var exception = dbSession.QueryOver<AnswerException>()
                    .Where(e => e.QuestionnaireID == answer.QuestionnaireID &&
                                e.QuestionID == answer.QuestionID &&
                                e.Value == answer.Value)
                    .Take(1).SingleOrDefault();

                return exception != null;
            }

            if (dbSession == null)
            {
                try
                {
                    dbSession = OpenSession();

                    return IsExceptionalAnswerInternal();
                }
                finally
                {
                    dbSession?.Dispose();
                }
            }
            else
                return IsExceptionalAnswerInternal();
        }
    }
}
