namespace QuestionaireApi
{
    public abstract class ResponseBase
    {
        public bool Success { get; set; }

        public object Ref { get; set; }
    }
}
