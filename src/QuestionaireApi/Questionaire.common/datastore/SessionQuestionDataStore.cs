namespace Questionaire.common.datastore
{
    public class SessionQuestionDataStore : DataStoreBase
    {
		public SessionQuestionDataStore(DataStoreConfig config)
            : base(config)
        {
		}

        public virtual Questionnaire GetQuestionnaire()
        {
            return QuestionaireDataStore.Instance
                .GetQuestionnaire(this.QuestionnaireID);
        }

        public virtual Session GetSession()
        {
            return SessionDataStore.Instance
                .GetSession(this.SessionID);
        }

        public virtual Question GetQuestion()
        {
            return QuestionDataStore.Instance
                .GetQuestion(this.QuestionID);
        }
    }
}

