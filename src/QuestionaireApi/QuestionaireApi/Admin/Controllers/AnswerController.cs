using System.IO;
using Microsoft.AspNetCore.Mvc;
using Questionaire.common.datastore;
using QuestionaireApi.Controllers;

namespace QuestionaireApi.Admin.Controllers
{
    [ApiController]
    [Route("admin/[controller]")]
    public class AnswerController : ControllerBaseCustom
    {
        public AnswerController()
            : base()
        {
        }

        [HttpGet("{questionnaireID}")]
        [HttpGet("{questionnaireID}/csv")]
        public string GetCsv(int questionnaireID) =>
            GetAnswer(questionnaireID)?.ToCsv();

        [HttpGet("{questionnaireID}/csv-file")]
        public Stream GetCsvFile(int questionnaireID)
        {
            var response = GetAnswer(questionnaireID);

            if (response == null)
                return null;

            using (var buffer = new FileStream(Path.GetTempFileName(), FileMode.CreateNew))
            {
                buffer.Write(System.Text
                    .Encoding.UTF8.GetBytes(
                        response.ToCsv()));

                return buffer;
            }
        }

        [HttpGet("{questionnaireID}/json")]
        public QuestionnaireAnswerRsp GetJson(int questionnaireID) =>
            GetAnswer(questionnaireID);

        private QuestionnaireAnswerRsp GetAnswer(int questionnaireID)
        {
            var questionnaire = QuestionnaireDataStore.Instance
                .GetQuestionnaire(questionnaireID);

            if (questionnaire == null)
                return null;

            return new QuestionnaireAnswerRsp()
            {
                Questions = questionnaire.GetQuestions(),
                Answers = questionnaire.GetAnswers(),
            };
        }
    }
}
