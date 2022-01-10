using System;
using Microsoft.Extensions.Caching.Memory;
using NHibernate;
using Questionaire.common.data;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class AnswerDataStore : DataStoreBase
    {
        private readonly IMemoryCache dataCache;
        private readonly AnswerExceptionDataStore answerExceptionDataStore;

        public AnswerDataStore(
            DataStoreConfig config,
            IMemoryCache dataCache,
            AnswerExceptionDataStore answerExceptionDataStore)
            : base(config)
        {
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
            using (var dbSession = OpenSession())
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

        /// <summary>
        /// Submit an answer and check whether the answer is exceptional answer on specified question.
        /// </summary>
        /// <param name="qSession">The session submitted from.</param>
        /// <param name="question">The question of submitting answer.</param>
        /// <param name="answer">The answer.</param>
        /// <param name="isException">Indicate whether the specified answer is exceptional answer.</param>
        /// <returns>True on success, False otherwise.</returns>
        public bool Submit(Session qSession, Question question, Answer answer, out bool isException)
        {
            using (var dbSession = OpenSession())
            using (var transaction = dbSession.BeginTransaction(System.Data.IsolationLevel.RepeatableRead))
            {
                var success = Submit(qSession, question, answer, dbSession, out isException);

                transaction.Commit();

                return success;
            }
        }

        /// <summary>
        /// Submit an answer using opened session and check whether the answer is exceptional answer on specified question.
        /// </summary>
        /// <param name="qSession">The session submitted from.</param>
        /// <param name="question">The question of submitting answer.</param>
        /// <param name="answer">The answer.</param>
        /// <param name="dbSession">The opened session.</param>
        /// <param name="isException">Indicate whether the specified answer is exceptional answer.</param>
        /// <returns>True on success, False otherwise.</returns>
        public bool Submit(Session qSession, Question question, Answer answer, ISession dbSession, out bool isException)
        {
            /// Check relation of <see cref="Session"/> , <see cref="Question"/> and <see cref="Answer"/>
            if (!qSession.QuestionnaireID.Equals(question.QuestionnaireID) ||
                !question.ID.Equals(answer.QuestionID))
                throw new InvalidOperationException($"Invalid submission!");

            var answerID = dbSession.Save(answer);

            isException = this.answerExceptionDataStore
                .IsExceptionalAnswer(answer, dbSession);

            return answerID != null;
        }
    }
}
