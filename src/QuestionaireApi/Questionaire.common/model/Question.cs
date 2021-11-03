using System;

namespace Questionaire.common.model
{
    public class Question
    {
        // TODO: Add section
        // TODO: Add property to indicate mix answers of text and choice

        public virtual int ID { get; set; }

        public virtual int QuestionnaireID { get; set; }

        public virtual string Label { get; set; }

        public virtual string Type { get; set; }

        public virtual int? ChoiceGroupID { get; set; }

        public virtual bool? IsMultiAnswer { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public Question()
        {
        }
    }
}
