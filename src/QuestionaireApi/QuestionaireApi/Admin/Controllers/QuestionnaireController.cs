using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<QuestionnaireRsp> List()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public QuestionnaireRsp Add(QuestionnaireReq request)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public QuestionnaireRsp Update(QuestionnaireReq request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{questionnaireID}")]
        public bool Delete(int questionnaireID)
        {
            throw new NotImplementedException();
        }
    }
}
