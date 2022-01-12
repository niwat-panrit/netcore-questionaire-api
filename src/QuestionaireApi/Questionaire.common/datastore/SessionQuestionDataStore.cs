using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class SessionQuestionDataStore : DataStoreBase
    {
        private readonly QuestionDataStore questionDataStore;

        public SessionQuestionDataStore(DataStoreConfig config,
            QuestionDataStore questionDataStore)
            : base(config)
        {
            this.questionDataStore = questionDataStore;
        }

        /// <summary>
        /// Get the first question of specified session.
        /// </summary>
        /// <param name="session">The session</param>
        /// <returns>The first question</returns>
        public Question GetFirstQuestion(Session session)
        {
            using (var dbSession = OpenSession())
            {
                var needle = dbSession.QueryOver<SessionQuestion>()
                    .Where(sq => sq.SessionID == session.ID)
                    .OrderBy(sq => sq.Sequence).Asc
                    .Take(1).SingleOrDefault();

                if (needle == null)
                    return null;

                return this.questionDataStore
                    .GetQuestion(needle.QuestionID);
            }
        }

        /// <summary>
        /// Get the next question from specified question.
        /// </summary>
        /// <param name="session">The session</param>
        /// <param name="question">The question</param>
        /// <returns>The next question</returns>
        public Question GetNextQuestion(Session session, Question question)
        {
            using (var dbSession = OpenSession())
            {
                var needle = dbSession.QueryOver<SessionQuestion>()
                    .Where(sq => sq.SessionID == session.ID &&
                                 sq.QuestionID == question.ID)
                    .Take(1).SingleOrDefault();

                if (needle == null)
                    return null;

                var nextNeedle = dbSession.QueryOver<SessionQuestion>()
                    .Where(sq => sq.SessionID == session.ID &&
                                 sq.Sequence > needle.Sequence)
                    .Take(1).SingleOrDefault();

                if (nextNeedle == null)
                    return null;

                return this.questionDataStore
                    .GetQuestion(nextNeedle.QuestionID);
            }
        }

        /// <summary>
        /// Determine whether the specified question is the last question of the session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="question">The question</param>
        /// <returns>True if the specified question is the last, False otherwise</returns>
        public bool IsLastQuestion(Session session, Question question) =>
            GetNextQuestion(session, question) == null;
    }
}
