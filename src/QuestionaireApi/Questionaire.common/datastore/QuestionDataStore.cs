using System;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class QuestionDataStore
    {
        public QuestionDataStore()
        {
        }

        public static Question GetFirstQuestion(Session session)
        {
            throw new NotImplementedException();
        }

        public static Question GetQuestion(Session session, int priorQuestionID)
        {
            throw new NotImplementedException();
        }

        public static object GetQuestion(int questionID)
        {
            throw new NotImplementedException();
        }
    }
}
