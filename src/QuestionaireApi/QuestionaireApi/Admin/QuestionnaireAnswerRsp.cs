using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Questionaire.common.model;

namespace QuestionaireApi.Admin
{
    public class QuestionnaireAnswerRsp
    {
        public IEnumerable<Question> Questions { get; set; }

        public IEnumerable<IEnumerable<Answer>> Answers { get; set; }

        public QuestionnaireAnswerRsp()
        {
        }

        public string ToCsv()
        {
            var buffer = new StringBuilder();

            // Header
            buffer.Append(
                string.Join(",", this.Questions
                    .Select(q => $"\"{q.Label}\"")))
                .Append(Environment.NewLine);

            // data
            foreach (var answer in this.Answers)
            {
                buffer.Append(
                    string.Join(",", answer
                        .Select(a => a.Value)))
                    .Append(Environment.NewLine);
            }

            return buffer.ToString();
        }
    }
}
