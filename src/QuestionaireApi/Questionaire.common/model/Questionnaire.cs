using System;

namespace Questionaire.common.model
{
    public class Questionnaire
    {
        public virtual int ID { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime ExpiredAt { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }

        public Questionnaire()
        {
        }
    }
}
