using System.Collections.Generic;
using Questionaire.common;

namespace QuestionaireApi.Admin
{
    public class ChoiceReq : RequestBase
    {
        public int? QuestionID { get; internal set; }

        public int? GroupID { get; internal set; }

        public string Text { get; internal set; }

        public ChoiceReq()
        {
        }

        public override bool Validate(List<KeyValuePair<string, string>> errors)
        {
            var success = true;

            if ((this.QuestionID is null or <= 0) &&
                (this.GroupID is null or <= 0))
            {
                success = false;
                var error = $"Either {nameof(QuestionID)} or {nameof(GroupID)} is required.";
                errors.Add(new(nameof(QuestionID), error));
                errors.Add(new(nameof(GroupID), error));
            }

            if (this.Text is null or { Length: 0 })
            {
                success = false;
                errors.Add(new(nameof(Text), SystemMessage.ValueRequired));
            }

            return success;
        }
    }
}
