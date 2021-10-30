using System;
using System.Collections.Generic;
using NHibernate;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class AnswerDataStore : DataStoreBase
    {
        private static AnswerDataStore _Instance;
        public static AnswerDataStore Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new AnswerDataStore();

                return _Instance;
            }
        }

        private Dictionary<int, ISession> dbSession =
            new Dictionary<int, ISession>();

        public AnswerDataStore()
            : base()
        {
        }

        public bool Submit(Session session, Question question, Answer newAnswer, bool more, List<string> warnings, out bool isException)
        {
            // TODO: Check relation of questionnaire, session and quest ids in answer

            ISession dbSession = null;
            try
            {
                if (!this.dbSession.TryGetValue(session.ID, out dbSession))
                {
                    dbSession = OpenSession();
                    dbSession.BeginTransaction();
                    this.dbSession.Add(session.ID, dbSession);
                }

                newAnswer.CreatedAt =
                    newAnswer.UpdatedAt =
                        DateTime.Now;
                dbSession.Save(newAnswer);

                // TODO: Check exception
                isException = false;

                return true;
            }
            finally
            {
                if (!more)
                {
                    dbSession?.GetCurrentTransaction().Commit();
                    dbSession?.Dispose();
                    this.dbSession.Remove(session.ID);
                }
            }
        }
    }
}
