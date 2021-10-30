using System;
using System.Collections.Generic;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class QuestionnaireDataStore : DataStoreBase
    {
        private static QuestionnaireDataStore _Instance;
        public static QuestionnaireDataStore Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new QuestionnaireDataStore();

                return _Instance;
            }
        }

        public QuestionnaireDataStore()
            : base()
        {
        }

        public IEnumerable<Questionnaire> GetAll()
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<Questionnaire>()
                    .List();
        }

        public Questionnaire GetQuestionnaire(int questionnaireID)
        {
            using (var dbSession = OpenSession())
            {
                return dbSession.QueryOver<Questionnaire>()
                    .Where(q => q.ID == questionnaireID)
                    .Take(1)
                    .SingleOrDefault();
            }
        }

        public Questionnaire Create(Questionnaire questionnaire)
        {
            throw new NotImplementedException();
        }

        public bool Update(Questionnaire qetionnaire)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Questionnaire qetionnaire)
        {
            throw new NotImplementedException();
        }
    }
}
