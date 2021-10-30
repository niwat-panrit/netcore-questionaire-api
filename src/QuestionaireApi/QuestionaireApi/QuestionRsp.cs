using System.Collections.Generic;
using Questionaire.common.model;

namespace QuestionaireApi
{
    public class QuestionRsp
    {
        public Session Session { get; set; }

        public Question Question { get; set; }

        public IEnumerable<Choice> Choices { get; set; }

        public string Text { get; set; }

        public QuestionRsp()
        {
        }

        //public QuestionRsp(Question question)
        //{
        //    this.Label = question.Label;
        //    this.Type = question.Type;
        //    this.IsMultiAnswer = question.IsMultiAnswer;
        //    if (this.Type.Equals(QuestionType.Choice))
        //    {
        //        var choices = question.ChoiceGroupID == null ?
        //            ChoiceDataStore.GetByQuestion(question) :
        //            ChoiceDataStore.GetByGroup((int)question.ChoiceGroupID);

        //        this.Choices = choices
        //            .Select(c => new ChoiceRsp(c));
        //    }

        //}
    }
}
