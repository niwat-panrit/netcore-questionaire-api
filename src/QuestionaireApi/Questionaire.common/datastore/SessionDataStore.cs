using System;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class SessionDataStore : DataStoreBase
    {
        // TODO: Optimize parameters

        private static SessionDataStore _Instance;
        public static SessionDataStore Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new SessionDataStore();

                return _Instance;
            }
        }

        public SessionDataStore()
            : base()
        {
        }

        public Session OpenQuestionnaireSession(int questionnaireID, string text = null)
        {
            Session qnSession;

            using (var dbSession = OpenStatelessSession())
            {
                var creationTime = DateTime.Now;
                qnSession = new Session()
                {
                    QuestionnaireID = questionnaireID,
                    IsCompleted = false,
                    Text = text,
                    CreatedAt = creationTime,
                    UpdatedAt = creationTime,
                };

                // TODO: Prepare list of question for this session

                qnSession.ID = (int)dbSession.Insert(qnSession);
            }

            return qnSession;
        }

        public void TerminateSession(int sessionID)
        {
            using (var dbSession = OpenSession())
            {
                var qnSession = dbSession.QueryOver<Session>()
                    .Where(s => s.ID == sessionID)
                    .Take(1).SingleOrDefault();

                if (qnSession == null)
                    return;

                qnSession.IsCompleted = true;
                qnSession.UpdatedAt = DateTime.Now;
                dbSession.Update(qnSession);
            }
        }

        public Question GetFirstQuestion(int sessionID)
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<SessionQuestion>()
                    .Where(sq => sq.SessionID == sessionID)
                    .OrderBy(sq => sq.Sequence).Asc
                    .Take(1).SingleOrDefault()
                    ?.GetQuestion();
        }

        public Question GetNextQuestion(int sessionID, int questionID)
        {
            using (var dbSession = OpenStatelessSession())
            {
                var needle = dbSession.QueryOver<SessionQuestion>()
                    .Where(sq => sq.SessionID == sessionID &&
                                 sq.QuestionID == questionID)
                    .Take(1).SingleOrDefault();

                if (needle == null)
                    return null;

                return dbSession.QueryOver<SessionQuestion>()
                    .Where(sq => sq.SessionID == sessionID &&
                                 sq.Sequence > needle.Sequence)
                    .Take(1).SingleOrDefault()
                    ?.GetQuestion();
            }
        }

        public Session GetSession(int sessionID)
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<Session>()
                    .Where(s => s.ID == sessionID)
                    .Take(1).SingleOrDefault();
        }
    }
}
