using Questionaire.common.datastore;

namespace Questionaire.common.model
{
    public class SessionQuestion
    {
        public virtual int SessionID { get; set; }

        public virtual int QuestionID { get; set; }

        public virtual int Sequence { get; set; }

        public SessionQuestion()
        {
        }

        public Question GetQuestion()
        {
            return QuestionDataStore.Instance
                .GetQuestion(this.QuestionID);
        }

        public Session GetSession()
        {
            return SessionDataStore.Instance
                .GetSession(this.SessionID);
        }

        #region Required by composite-id

        public override bool Equals(object obj)
        {
            if (obj is null ||
                obj is not SessionQuestion)
                return false;

            var another = obj as SessionQuestion;
            return this.SessionID.Equals(another.SessionID) &&
                this.QuestionID.Equals(another.QuestionID);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
