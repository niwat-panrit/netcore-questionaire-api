using System;

namespace Questionaire.common.model
{
    public class Session
    {
        public virtual int ID { get; set; }
        public virtual int QuestionnaireID { get; set; }
        public virtual bool IsCompleted { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }

        public Session()
        {
        }

        public bool IsLastQuestion(object question)
        {
            throw new NotImplementedException();
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }

        public int GetNextQuestion(object question)
        {
            throw new NotImplementedException();
        }
    }
}
