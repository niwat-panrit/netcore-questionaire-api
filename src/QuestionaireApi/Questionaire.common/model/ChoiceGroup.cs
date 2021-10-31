using System;

namespace Questionaire.common.model
{
    public class ChoiceGroup
    {
        public virtual int ID { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public ChoiceGroup()
        {
        }
    }
}
