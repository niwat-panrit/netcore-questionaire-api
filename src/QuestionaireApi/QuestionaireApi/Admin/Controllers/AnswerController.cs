using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{questionnaireID}/json")]
        public IEnumerable<QuestionnaireAnswerRsp> GetJson(int questionnaireID) =>
            Get(questionnaireID);

        [HttpGet("{questionnaireID}/csv")]
        public string GetCsv(int questionnaireID)
        {
            var answers = Get(questionnaireID);

            throw new NotImplementedException();
        }

        private IEnumerable<QuestionnaireAnswerRsp> Get(int questionnaireID)
        {
            throw new NotImplementedException();
        }
    }
}
