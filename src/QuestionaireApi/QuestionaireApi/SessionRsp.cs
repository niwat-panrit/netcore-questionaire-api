using Questionaire.common.model;

namespace QuestionaireApi
{
    public class SessionRsp
    {
        public int? ID { get; set; }

        public string Text { get; set; }

        public SessionRsp(Session session)
        {
            if (session == null)
                this.Text = "Session isn't available.";
            else
                this.ID = session.ID;
        }
    }
}
