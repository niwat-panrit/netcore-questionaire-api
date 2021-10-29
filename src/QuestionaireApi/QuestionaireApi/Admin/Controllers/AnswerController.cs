using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuestionaireApi.Controllers;

namespace QuestionaireApi.Admin.Controllers
{
    [ApiController]
    [Route("[admin/answer]")]
    public class AnswerController : ControllerBaseCustom
    {
        public AnswerController()
            : base()
        {
        }

        [HttpGet]
        public string GetJson(int questionnaireID) =>
            JsonConvert.SerializeObject(Get(questionnaireID));

        [HttpGet]
        public string GetCsv(int questionnaireID)
        {
            var answers = Get(questionnaireID);

            throw new NotImplementedException();
        }

        private List<QuestionnaireAnswerRsp> Get(int questionnaireID)
        {
            throw new NotImplementedException();
        }
    }
}
