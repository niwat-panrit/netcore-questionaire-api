using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuestionaireApi.Controllers;

namespace QuestionaireApi.Admin.Controllers
{
    [ApiController]
    [Route("[admin/choice]")]
    public class ChoiceController : ControllerBaseCustom
    {
        public ChoiceController()
            : base()
        {
        }

        [HttpGet]
        public IEnumerable<ChoiceRsp> ListByQuestion(int questionID)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IEnumerable<ChoiceRsp> ListByGroup(int groupID)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ChoiceRsp Add(ChoiceReq request)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ChoiceRsp Update(ChoiceReq request)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public bool Delete(int choiceID)
        {
            throw new NotImplementedException();
        }
    }
}
