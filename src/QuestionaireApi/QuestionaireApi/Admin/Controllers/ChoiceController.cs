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
    public class ChoiceController : ControllerBaseCustom
    {
        // TODO: Load post values into request object

        public ChoiceController()
            : base()
        {
        }

        [HttpGet("by-question/{questionID}")]
        public IEnumerable<Choice> ListByQuestion(int questionID)
        {
            var question = QuestionDataStore.Instance
                .GetQuestion(questionID);

            if (question == null)
                return null;

            return ChoiceDataStore.Instance
                .GetByQuestion(question);
        }
            

        [HttpGet("by-group/{groupID}")]
        public IEnumerable<Choice> ListByGroup(int groupID) =>
            ChoiceDataStore.Instance
                .GetByGroup(groupID);

        [HttpPut]
        public ChoiceRsp Add(ChoiceReq request)
        {
            var errors = new List<KeyValuePair<string, string>>();

            if (!request.Validate(errors))
                return new ChoiceRsp()
                {
                    Success = false,
                    Ref = errors,
                };

            var newChoice = ChoiceDataStore.Instance
                .Create(new Choice()
                {
                    QuestionID = request.QuestionID,
                    GroupID = request.GroupID,
                    Text = request.Text,
                });

            return new ChoiceRsp()
            {
                Success = newChoice != null,
                Ref = newChoice,
            };
        }

        [HttpPatch("{choiceID}")]
        public ChoiceRsp Update(int choiceID, ChoiceReq request)
        {
            var choice = ChoiceDataStore.Instance
                .GetChoice(choiceID);

            if (choice == null)
                return new ChoiceRsp()
                {
                    Success = false,
                    Ref = SystemMessage.ChoiceNotFoundInvalid,
                };

            var errors = new List<KeyValuePair<string, string>>();

            if (!request.Validate(errors))
                return new ChoiceRsp()
                {
                    Success = false,
                    Ref = errors,
                };

            choice.QuestionID = request.QuestionID;
            choice.GroupID = request.GroupID;
            choice.Text = request.Text;;

            return new ChoiceRsp()
            {
                Success = ChoiceDataStore.Instance
                    .Update(choice),
                Ref = choice,
            };
        }

        [HttpDelete("{choiceID}")]
        public ChoiceRsp Delete(int choiceID)
        {
            var choice = ChoiceDataStore.Instance
                .GetChoice(choiceID);

            if (choice == null)
                return new ChoiceRsp()
                {
                    Success = false,
                    Ref = SystemMessage.ChoiceNotFoundInvalid,
                };

            return new ChoiceRsp()
            {
                Success = ChoiceDataStore.Instance
                    .Delete(choice),
            };
        }
    }
}
