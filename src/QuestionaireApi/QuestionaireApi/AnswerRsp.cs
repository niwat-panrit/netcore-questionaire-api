using Questionaire.common.model;

namespace QuestionaireApi
{
    public class AnswerRsp
    {
        public Session Session { get; set; }

        public bool IsAccepted { get; set; }

        public int? NextQuestionID { get; set; }

        public string Text { get; set; }

        public AnswerRsp()
        {
        }
    }
}
