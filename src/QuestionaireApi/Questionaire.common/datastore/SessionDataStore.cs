using System;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class SessionDataStore : DataStoreBase
    {
        public SessionDataStore(DataStoreConfig config)
            : base(config)
        {
        }

        /// <summary>
        /// Open new questionnaire session.
        /// </summary>
        /// <param name="questionnaire">The questionnaire</param>
        /// <param name="text">Remark for opening session</param>
        /// <returns>The new questionnaire session</returns>
        public Session OpenQuestionnaireSession(Questionnaire questionnaire, string text = null)
        {
            Session qnSession;

            using (var dbSession = OpenSession())
            {
                // TODO: Use DB's on create and update
                var now = DateTime.Now;
                qnSession = new Session()
                {
                    QuestionnaireID = questionnaire.ID,
                    IsCompleted = false,
                    Text = text,
                    CreatedAt = now,
                    UpdatedAt = now,
                };

                dbSession.Save(qnSession);

                // TODO: Prepare list of question for this session
                //qnSession.PrepareQuestionList();
            }

            return qnSession;
        }

        /// <summary>
        /// Terminate opened session.
        /// </summary>
        /// <param name="session"></param>
        public void TerminateSession(Session session)
        {
            using (var dbSession = OpenSession())
            {
                var qnSession = dbSession.QueryOver<Session>()
                    .Where(s => s.ID == session.ID)
                    .Take(1).SingleOrDefault();

                if (qnSession == null)
                    return;

                qnSession.IsCompleted = true;

                // TODO: Use DB's on update
                qnSession.UpdatedAt = DateTime.Now;

                dbSession.Update(qnSession);
            }
        }

        /// <summary>
        /// Get the session by identifier.
        /// </summary>
        /// <param name="sessionID">The identifier</param>
        /// <returns>The session</returns>
        public Session GetSession(int sessionID)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Session>()
                    .Where(s => s.ID == sessionID)
                    .Take(1).SingleOrDefault();
        }
    }
}
