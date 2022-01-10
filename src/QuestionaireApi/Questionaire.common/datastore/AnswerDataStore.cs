using System;
using Microsoft.Extensions.Caching.Memory;
using NHibernate;
using Questionaire.common.data;
using Questionaire.common.model;
using Questionaire.common.tool;

namespace Questionaire.common.datastore
{
    public class AnswerDataStore : DataStoreBase
    {
        private readonly ICache<ISession> sessionCache;
        private readonly IMemoryCache dataCache;
        private readonly AnswerExceptionDataStore answerExceptionDataStore;

        public AnswerDataStore(
            DataStoreConfig config,
            ICache<ISession> sessionCache,
            IMemoryCache dataCache,
            AnswerExceptionDataStore answerExceptionDataStore)
            : base(config)
        {
            this.sessionCache = sessionCache;
            this.dataCache = dataCache;
            this.answerExceptionDataStore = answerExceptionDataStore;
        }

        /// <summary>
        /// Get all submitted answers of specified <see cref="Questionnaire"/>.
        /// </summary>
        /// <param name="questionaire">Querying <see cref="Questionnaire"/>.</param>
        /// <returns>
        /// Answers of <see cref="Questionnaire"/> group by question.
        /// (Some <see cref="Question"/> may has multiple answers.)
        /// </returns>
        public QuestionaireResultSet GetAnswers(Questionnaire questionaire)
        {
            var cacheKey = questionaire;

            // Try to get result from the cache for faster result
            if (this.dataCache.TryGetValue(cacheKey, out QuestionaireResultSet cachedResult))
                return cachedResult;

            // Cache isn't available, build new result
            using (var dbSession = OpenStatelessSession())
            using (var transaction = dbSession.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                var questions = dbSession.QueryOver<Question>()
                    .Where(q => q.QuestionnaireID == questionaire.ID)
                    .OrderBy(q => q.DisplayOrder).Asc
                    .ThenBy(q => q.CreatedAt).Asc
                    .List();
                var result = new QuestionaireResultSet(questions);
                var answers = dbSession.QueryOver<Answer>()
                    .Where(a => a.QuestionnaireID == questionaire.ID)
                    .OrderBy(a => a.CreatedAt).Asc
                    .List();
                result.Read(answers);

                // Cache result in the memory for 30 seconds
                // TODO: Configurable cache period
                this.dataCache.Set(cacheKey, result, TimeSpan.FromSeconds(30));

                return result;
            }
        }

        public bool Submit(Session qSession, Question question, Answer answer, bool more, out bool isException)
        {
            if (!qSession.QuestionnaireID.Equals(question.QuestionnaireID) ||
                !question.ID.Equals(answer.QuestionID))
                throw new InvalidOperationException($"Invalid submission!");

            var cacheKey = question;

            try
            {
                var dbSession = this.sessionCache.Get(cacheKey);

                var answerID = dbSession.Save(answer);
                isException = this.answerExceptionDataStore
                    .IsExceptionalAnswer(answer, dbSession);

                return answerID != null;
            }
            finally
            {
                if (!more)
                    this.sessionCache.Clear(cacheKey);
            }
        }
    }
}
