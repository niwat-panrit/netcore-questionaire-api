using System;
using System.Collections.Generic;
using System.Data;
using Questionaire.common.model;

namespace Questionaire.common.data
{
    public class QuestionaireResultSet
	{
        private readonly Dictionary<int, Question> questions =
            new Dictionary<int, Question>();

        /// <summary>
        /// Rows and Columns of answers:
        ///     Top row is header which is list of question's title.
        ///     Each row (Row ID = Session ID) represent a set of answer from the Questionnaire's questions.
        ///     Each column (Column ID = Question ID) represent answer(s) of each question.
        /// </summary>
        public DataTable AnswerSheet { get; } =
            new DataTable();

        /// <summary>
        /// Instantiate new object of <see cref="QuestionaireResultSet"/>
        /// </summary>
        /// <param name="questions"><see cref="QuestionaireResultSet"/>'s <see cref="Question"/>s</param>
        public QuestionaireResultSet(IList<Question> questions)
        {
            var headerRow = this.AnswerSheet.NewRow();

            foreach (var question in questions)
            {
                if (this.questions.ContainsKey(question.ID))
                    throw new InvalidOperationException($"Duplicated question(s)!");

                this.questions.Add(question.ID, question);

                var columnID = question.ID.ToString();
                this.AnswerSheet.Columns
                    .Add(columnID, typeof(AnswerSet));
                headerRow[columnID] = question.Label;
            }

            this.AnswerSheet.Rows.Add(headerRow);
        }

        /// <summary>
        /// Read specified <see cref="Answer"/>s into the <see cref="AnswerSheet"/>
        /// </summary>
        /// <param name="answers">List of <see cref="Answer"/></param>
        /// <exception cref="InvalidOperationException">When <see cref="Answer"/> doesn't belong to any <see cref="Question"/> in the <see cref="Questionaire"/></exception>
        public void Read(IList<Answer> answers)
        {
            /// Session ID => associated <see cref="DataRow"/>
            var dataRowIndex = new Dictionary<int, DataRow>();

            foreach (var record in answers)
            {
                /// Get reference to the source question and also validate relation between <see cref="Answer"/> and <see cref="Question"/>
                if (!this.questions.TryGetValue(record.QuestionID, out Question sourceQuestion))
                    throw new InvalidOperationException($"The answer doesn't belong to any question in the questionnaire!");

                if (!dataRowIndex.TryGetValue(record.SessionID, out DataRow dataRow))
                    dataRowIndex.Add(record.SessionID, dataRow = this.AnswerSheet.NewRow()); // Add new data row into the sheet

                var columndID = record.QuestionID.ToString();
                var answerSet = (AnswerSet)dataRow[columndID];
                if (answerSet != null)
                    dataRow[columndID] = // Add new column into the row
                        answerSet = new AnswerSet(sourceQuestion);
                answerSet.Answers.Add(record); // Add value into the column
            }
        }

        public class AnswerSet
        {
            public Question Question { get; }

            public List<Answer> Answers { get; } =
                new List<Answer>();

            public AnswerSet(Question sourceQuestion)
            {
                this.Question = sourceQuestion;
            }
        }
    }
}
