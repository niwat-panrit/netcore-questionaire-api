using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuestionaireApi.Controllers;

namespace QuestionaireApi.Admin.Controllers
{
    [ApiController]
    [Route("admin/[controller]")]
    public class QuestionController : ControllerBaseCustom
    {
        public QuestionController()
            : base()
        {
        }

        [HttpGet("{questionnaireID}")]
        public IEnumerable<QuestionRsp> List(int questionnaireID)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{questionnaireID}")]
        public QuestionRsp Add(int questionnaireID, QuestionReq request)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{questionnaireID}")]
        public QuestionRsp Update(int questionnaireID, QuestionReq request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{questionnaireID}/{questionID}")]
        public bool Delete(int questionnaireID, int questionID)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{questionnaireID}/swap/{questionID1}/{questionID2}")]
        public bool SwapOrder(int questionnaireID, int questionID1, int questionID2)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{questionnaireID}/move-up/{questionID}")]
        public bool MoveUpOrder(int questionnaireID, int questionID)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{questionnaireID}/move-down/{questionID}")]
        public bool MoveDownOrder(int questionnaireID, int questionID)
        {
            throw new NotImplementedException();
        }
    }
}
