﻿using System;
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

        public IEnumerable<Questionnaire> GetAllQuestionnaires()
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<Questionnaire>()
                    .List();
        }

        public Questionnaire GetQuestionnaire(int questionnaireID)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Questionnaire>()
                    .Where(q => q.ID == questionnaireID)
                    .Take(1).SingleOrDefault();
        }

        public Questionnaire Create(Questionnaire questionnaire)
        {
            using (var dbSession = OpenStatelessSession())
            {
                questionnaire.CreatedAt =
                    questionnaire.UpdatedAt =
                        DateTime.Now;
                questionnaire.ID = (int)dbSession.Insert(questionnaire);

                return questionnaire;
            }
        }

        public bool Update(Questionnaire qetionnaire)
        {
            using (var dbSession = OpenSession())
            {
                qetionnaire.UpdatedAt = DateTime.Now;
                dbSession.Update(qetionnaire);

                return true;
            }
        }

        public bool Delete(Questionnaire qetionnaire)
        {
            using (var dbSession = OpenSession())
            {
                dbSession.Delete(qetionnaire);

                return true;
            }
        }
    }
}
