using System;

namespace Questionaire.common.model
{
    public class Choice
    {
        public virtual int ID { get; set; }

        public virtual int? QuestionID { get; set; }

        public virtual int? GroupID { get; set; }

        public virtual string Text { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public Choice()
        {
        }
    }
}
