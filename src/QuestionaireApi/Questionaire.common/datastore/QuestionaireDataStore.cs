using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using NHibernate.Util;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class QuestionaireDataStore : DataStoreBase
    {
        public QuestionaireDataStore(DataStoreConfig config)
            : base(config)
        {
        }

        /// <summary>
        /// Get all questionnaires.
        /// </summary>
        /// <returns>List of questionnaires</returns>
        public IList<Questionnaire> GetAllQuestionnaires()
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Questionnaire>()
                    .List();
        }

        /// <summary>
        /// Get questionnaire by identifier.
        /// </summary>
        /// <param name="questionnaireID">The questionnaire identifier</param>
        /// <returns>The questionnaire</returns>
        public Questionnaire GetQuestionnaire(int questionnaireID)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Questionnaire>()
                    .Where(q => q.ID == questionnaireID)
                    .Take(1).SingleOrDefault();
        }

        /// <summary>
        /// Get previous question of specified question in the questionnaire.
        /// </summary>
        /// <param name="question">The question</param>
        /// <returns>Previous question</returns>
        public Question GetPreviousQuestion(Question question)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Question>()
                    .Where(q => q.DisplayOrder < question.DisplayOrder)
                    .OrderBy(q => q.DisplayOrder).Desc
                    .Take(1).SingleOrDefault();
        }

        /// <summary>
        /// Get next question of specified question in the questionnaire.
        /// </summary>
        /// <param name="question">The question</param>
        /// <returns>Next question</returns>
        public Question GetNextQuestion(Question question)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Question>()
                    .Where(q => q.DisplayOrder > question.DisplayOrder)
                    .OrderBy(q => q.DisplayOrder).Asc
                    .Take(1).SingleOrDefault();
        }

        /// <summary>
        /// Get the lowest question's display order in the list of questions of specified questionnaire.
        /// </summary>
        /// <param name="questionnaire">The questionnaire</param>
        /// <returns>Lowest display order</returns>
        public int GetMinDisplayOrder(Questionnaire questionnaire)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Question>()
                    .Select(
                        Projections
                           .ProjectionList().Add(
                                Projections.Min<Question>(q => q.DisplayOrder)))
                    .Where(q => q.QuestionnaireID == questionnaire.ID)
                    .List<int>().First();
        }

        /// <summary>
        /// Get the most question's display order in the list of questions of specified questionnaire.
        /// </summary>
        /// <param name="questionaire">The questionnaire</param>
        /// <returns>Most display order</returns>
        public int GetMaxDisplayOrder(Questionnaire questionaire)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Question>()
                    .Select(
                        Projections
                           .ProjectionList().Add(
                                Projections.Max<Question>(q => q.DisplayOrder)))
                    .Where(q => q.QuestionnaireID == questionaire.ID)
                    .List<int>().First();
        }

        /// <summary>
        /// Save new questionnaire.
        /// </summary>
        /// <param name="questionnaire">The new questionnaire</param>
        public void Create(Questionnaire questionnaire)
        {
            using (var dbSession = OpenSession())
            {
                // TODO: Use DB's on create and update
                questionnaire.CreatedAt =
                    questionnaire.UpdatedAt =
                        DateTime.Now;

                dbSession.Save(questionnaire);
            }
        }

        /// <summary>
        /// Update specified questionnaire
        /// </summary>
        /// <param name="qetionnaire">The questionnaire</param>
        public void Update(Questionnaire qetionnaire)
        {
            using (var dbSession = OpenSession())
            {
                // TODO: Use DB's on update
                qetionnaire.UpdatedAt = DateTime.Now;

                dbSession.Update(qetionnaire);
            }
        }

        /// <summary>
        /// Delete specified questionnaire
        /// </summary>
        /// <param name="qetionnaire">The questionnaire</param>
        public void Delete(Questionnaire qetionnaire)
        {
            using (var dbSession = OpenSession())
            {
                dbSession.Delete(qetionnaire);
            }
        }
    }
}
