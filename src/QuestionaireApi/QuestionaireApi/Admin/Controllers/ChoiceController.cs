using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuestionaireApi.Controllers;

namespace QuestionaireApi.Admin.Controllers
{
    [ApiController]
    [Route("admin/[controller]")]
    public class ChoiceController : ControllerBaseCustom
    {
        public ChoiceController()
            : base()
        {
        }

        [HttpGet("by-question/{questionID}")]
        public IEnumerable<ChoiceRsp> ListByQuestion(int questionID)
        {
            throw new NotImplementedException();
        }

        [HttpGet("by-group/{groupID}")]
        public IEnumerable<ChoiceRsp> ListByGroup(int groupID)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public ChoiceRsp Add(ChoiceReq request)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public ChoiceRsp Update(ChoiceReq request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{choiceID}")]
        public bool Delete(int choiceID)
        {
            throw new NotImplementedException();
        }
    }
}
