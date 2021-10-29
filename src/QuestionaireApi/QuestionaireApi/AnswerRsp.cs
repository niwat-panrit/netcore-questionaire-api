namespace QuestionaireApi
{
    public class AnswerRsp
    {
        public bool IsAccepted { get; set; }

        public bool IsSessionTerminated { get; set; }

        public int? NextQuestionID { get; set; }

        public string Text { get; set; }

        public AnswerRsp()
        {
        }
    }
}
