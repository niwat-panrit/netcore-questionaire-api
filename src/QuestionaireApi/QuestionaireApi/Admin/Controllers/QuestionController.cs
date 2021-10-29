using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuestionaireApi.Controllers;

namespace QuestionaireApi.Admin.Controllers
{
    [ApiController]
    [Route("[admin/question]")]
    public class QuestionController : ControllerBaseCustom
    {
        public QuestionController()
            : base()
        {
        }

        [HttpGet]
        public IEnumerable<QuestionRsp> List(int questionnaireID)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public QuestionRsp Add(int questionnaireID, QuestionReq request)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public QuestionRsp Update(int questionnaireID, QuestionReq request)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public bool Delete(int questionnaireID, int questionID)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public bool SwapOrder(int questionnaireID, int questionID1, int questionID2)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public bool MoveUpOrder(int questionnaireID, int questionID1)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public bool MoveDownOrder(int questionnaireID, int questionID1)
        {
            throw new NotImplementedException();
        }
    }
}
