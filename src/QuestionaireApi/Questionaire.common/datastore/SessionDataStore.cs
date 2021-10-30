using System;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class SessionDataStore : DataStoreBase
    {
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

        public Session GetSession(int sessionID)
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<Session>()
                    .Where(s => s.ID == sessionID)
                    .Take(1)
                    .SingleOrDefault();
        }

        public Session OpenQuestionnaireSession(int questionnaireID)
        {
            Session qnSession;

            using (var dbSession = OpenStatelessSession())
            using (var transaction = dbSession.BeginTransaction())
            {
                qnSession = new Session()
                {
                    QuestionnaireID = questionnaireID,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                dbSession.Insert(qnSession);
                transaction.Commit();
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
                dbSession.SaveOrUpdate(qnSession);
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
    }
}
