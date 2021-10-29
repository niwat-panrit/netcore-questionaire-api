using System;
using System.Collections.Generic;

namespace QuestionaireApi
{
    public class AnswerReq
    {
        public AnswerReq()
        {
        }

        internal bool Validate(List<string> warnings)
        {
            throw new NotImplementedException();
        }

        internal bool Save(Questionaire.common.model.Session session, object question, List<string> warnings, out bool isException)
        {
            throw new NotImplementedException();
        }
    }
}
