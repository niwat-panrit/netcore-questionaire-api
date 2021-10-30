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
    }
}
