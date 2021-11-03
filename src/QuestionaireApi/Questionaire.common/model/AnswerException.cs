using System;

namespace Questionaire.common.model
{
    public class AnswerException
    {
        // TODO: Add exception note

        public virtual int ID { get; set; }

        public virtual int QuestionnaireID { get; set; }

        public virtual int QuestionID { get; set; }

        public virtual string Value { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public AnswerException()
        {
        }
    }
}
