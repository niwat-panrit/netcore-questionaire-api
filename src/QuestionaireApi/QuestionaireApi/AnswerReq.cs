using System;
using System.Collections.Generic;
using Questionaire.common;
using Questionaire.common.model;

namespace QuestionaireApi
{
    public class AnswerReq : RequestBase
    {
        public int QuestionnaireID { get; set; }

        public int SessionID { get; set; }

        public int QuestionID { get; set; }

        public string Value { get; set; }

        public AnswerReq()
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

            if (this.SessionID <= 0)
            {
                success = false;
                errors.Add(new(nameof(SessionID), SystemMessage.ValueInvalid));
            }

            if (this.QuestionID <= 0)
            {
                success = false;
                errors.Add(new(nameof(QuestionID), SystemMessage.ValueInvalid));
            }

            if (this.Value is null or { Length: 0 })
            {
                success = false;
                errors.Add(new(nameof(Value), SystemMessage.ValueRequired));
            }

            return success;
        }

        public bool Save(Session session, Question question, List<string> warnings, out bool isException)
        {
            throw new NotImplementedException();
        }
    }
}
