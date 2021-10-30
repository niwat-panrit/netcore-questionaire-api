using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Questionaire.common;
using Questionaire.common.datastore;
using Questionaire.common.model;
using QuestionaireApi.Controllers;

namespace QuestionaireApi.Admin.Controllers
{
    [ApiController]
    [Route("admin/[controller]")]
    public class QuestionnaireController : ControllerBaseCustom
    {
        public QuestionnaireController()
            : base()
        {
        }

        [HttpGet]
        public IEnumerable<Questionnaire> List() =>
            QuestionnaireDataStore.Instance
                .GetAllQuestionnaires();

        [HttpPut]
        public QuestionnaireRsp Add(QuestionnaireReq request)
        {
            var errors = new List<KeyValuePair<string, string>>();

            if (!request.Validate(errors))
                return new QuestionnaireRsp()
                {
                    Success = false,
                    Ref = errors,
                };

            var newQuestionnaire = QuestionnaireDataStore.Instance
                .Create(new Questionnaire()
                {
                    Title = request.Title,
                    Description = request.Description,
                    ExpiredAt = request.ExpiredAt,
                });

            return new QuestionnaireRsp()
            {
                Success = newQuestionnaire != null,
                Ref = newQuestionnaire,
            };
        }

        [HttpPatch("{questionnaireID}")]
        public QuestionnaireRsp Update(int questionnaireID, QuestionnaireReq request)
        {
            var qetionnaire = QuestionnaireDataStore.Instance
                .GetQuestionnaire(questionnaireID);

            if (qetionnaire == null)
                return new QuestionnaireRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionnaireNotFoundInvalid,
                };

            var errors = new List<KeyValuePair<string, string>>();

            if (!request.Validate(errors))
                return new QuestionnaireRsp()
                {
                    Success = false,
                    Ref = errors,
                };

            qetionnaire.Title = request.Title;
            qetionnaire.Description = request.Description;
            qetionnaire.ExpiredAt = request.ExpiredAt;

            return new QuestionnaireRsp()
            {
                Success = QuestionnaireDataStore.Instance
                    .Update(qetionnaire),
                Ref = qetionnaire,
            };
        }

        [HttpDelete("{questionnaireID}")]
        public QuestionnaireRsp Delete(int questionnaireID)
        {
            var qetionnaire = QuestionnaireDataStore.Instance
                .GetQuestionnaire(questionnaireID);

            if (qetionnaire == null)
                return new QuestionnaireRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionnaireNotFoundInvalid,
                };

            return new QuestionnaireRsp()
            {
                Success = QuestionnaireDataStore.Instance
                    .Delete(qetionnaire),
            };
        }
    }
}
