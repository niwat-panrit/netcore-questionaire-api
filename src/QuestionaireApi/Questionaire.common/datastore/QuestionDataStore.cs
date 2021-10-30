using System;
using System.Collections.Generic;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class QuestionDataStore : DataStoreBase
    {
        private static QuestionDataStore _Instance;
        public static QuestionDataStore Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new QuestionDataStore();

                return _Instance;
            }
        }

        public QuestionDataStore()
            : base()
        {
        }

        public Question GetQuestion(int questionID)
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<Question>()
                    .Where(q => q.ID == questionID)
                    .Take(1)
                    .SingleOrDefault();
        }

        public IEnumerable<Question> GetQuestions(int questionnaireID)
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<Question>()
                    .Where(q => q.QuestionnaireID == questionnaireID)
                    .OrderBy(q => q.DisplayOrder).Asc
                    .List();
        }

        public Question Create(Question question)
        {
            throw new NotImplementedException();
        }

        public bool Update(Question question)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Question question)
        {
            throw new NotImplementedException();
        }
    }
}
