using System;
using Questionaire.common.datastore;

namespace Questionaire.common.model
{
    public class Session
    {
        public virtual int ID { get; set; }

        public virtual int QuestionnaireID { get; set; }

        public virtual bool IsCompleted { get; set; }

        public virtual string Text { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public Session()
        {
        }

        public Question GetFirstQuestion()
        {
            return SessionDataStore.Instance
                .GetFirstQuestion(this.ID);
        }

        public Question GetNextQuestion(Question question)
        {
            return SessionDataStore.Instance
                .GetNextQuestion(this.ID, question.ID);
        }

        public bool IsLastQuestion(Question question)
        {
            return GetNextQuestion(question) == null;
        }

        public void Terminate()
        {
            SessionDataStore.Instance
                .TerminateSession(this.ID);
        }
    }
}
