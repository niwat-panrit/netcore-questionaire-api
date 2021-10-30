using System.Collections.Generic;
using Questionaire.common;

namespace QuestionaireApi.Admin
{
    public class QuestionReq : RequestBase
    {
        public int QuestionnaireID { get; internal set; }

        public string Label { get; internal set; }

        public string Type { get; internal set; }

        public bool? IsMultiAnswer { get; internal set; }

        public int? ChoiceGroupID { get; internal set; }

        public int DisplayOrder { get; internal set; }

        public QuestionReq()
        {
        }

        public override bool Validate(List<KeyValuePair<string, string>> errors)
        {
            var success = true;

            if (this.QuestionnaireID <= 0)
            {
                success = false;
                errors.Add(new(nameof(QuestionnaireID), SystemMessage.ValueInvalid));
            }

            if (this.Label is null or { Length: 0 })
            {
                success = false;
                errors.Add(new(nameof(Label), SystemMessage.ValueRequired));
            }

            if (this.Type is null or { Length: 0 })
            {
                success = false;
                errors.Add(new(nameof(Type), SystemMessage.ValueRequired));
            }
            else if (this.Type is
                    not QuestionType.Text and
                    not QuestionType.Choice)
            {
                success = false;
                errors.Add(new(nameof(Type), SystemMessage.ValueInvalid));
            }

            if (this.DisplayOrder <= 0)
            {
                success = false;
                errors.Add(new(nameof(DisplayOrder), SystemMessage.ValueInvalid));
            }

            return success;
        }
    }
}
