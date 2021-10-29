using System.Collections.Generic;
using System.Linq;
using Questionaire.common;
using Questionaire.common.datastore;
using Questionaire.common.model;

namespace QuestionaireApi
{
    public class QuestionRsp
    {
        public bool IsSessionTerminated { get; set; }

        public string Label { get; set; }

        public string Type { get; set; }

        public bool? IsMultiAnswer { get; set; }

        public IEnumerable<ChoiceRsp> Choices { get; set; }

        public string Text { get; set; }

        public QuestionRsp()
        {
        }

        public QuestionRsp(Question question)
        {
            this.Label = question.Label;
            this.Type = question.Type;
            this.IsMultiAnswer = question.IsMultiAnswer;
            if (this.Type.Equals(QuestionType.Choice))
            {
                var choices = question.ChoiceGroupID == null ?
                    ChoiceDataStore.GetByQuestion(question) :
                    ChoiceDataStore.GetByGroup((int)question.ChoiceGroupID);

                this.Choices = choices
                    .Select(c => new ChoiceRsp(c));
            }

        }
    }
}
