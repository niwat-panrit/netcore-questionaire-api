using System;
using System.Collections.Generic;
using Questionaire.common;

namespace QuestionaireApi.Admin
{
    public class QuestionnaireReq : RequestBase
    {
        public string Title { get; internal set; }

        public string Description { get; internal set; }

        public DateTime ExpiredAt { get; internal set; }

        public QuestionnaireReq()
        {
        }

        public override bool Validate(List<KeyValuePair<string, string>> errors)
        {
            var success = true;

            if (this.Title.Trim() is null or { Length: 0 })
            {
                success = false;
                errors.Add(new(nameof(Title), SystemMessage.ValueRequired));
            }

            return success;
        }
    }
}
