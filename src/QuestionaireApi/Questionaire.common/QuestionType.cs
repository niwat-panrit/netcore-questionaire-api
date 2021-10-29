namespace Questionaire.common
{
    public class QuestionType
    {
        public const string Text = "TEXT";
        public const string Choice = "CHOICE";

        public static bool IsValid(string typeID) =>
            typeID.Equals(Text) ||
            typeID.Equals(Choice);
    }
}
