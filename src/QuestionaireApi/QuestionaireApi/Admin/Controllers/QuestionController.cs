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
    public class QuestionController : ControllerBaseCustom
    {
        public QuestionController()
            : base()
        {
        }

        [HttpGet("{questionnaireID}")]
        public IEnumerable<Question> List(int questionnaireID)
        {
            var questionnaire = QuestionnaireDataStore.Instance
                .GetQuestionnaire(questionnaireID);

            return questionnaire?.GetQuestions();
        }

        [HttpPut("{questionnaireID}")]
        public QuestionRsp Add(int questionnaireID, QuestionReq request)
        {
            var questionnaire = QuestionnaireDataStore.Instance
                .GetQuestionnaire(questionnaireID);

            if (questionnaire == null)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionnaireExpiredOrInvalid,
                };

            var errors = new List<KeyValuePair<string, string>>();

            if (!request.Validate(errors))
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = errors,
                };

            var newQuestion = QuestionDataStore.Instance
                .Create(new Question()
                {
                    QuestionnaireID = request.QuestionnaireID,
                    Label = request.Label,
                    Type = request.Type,
                    IsMultiAnswer = request.IsMultiAnswer,
                    ChoiceGroupID = request.ChoiceGroupID,
                    DisplayOrder = request.DisplayOrder,
                });

            return new QuestionRsp()
            {
                Success = newQuestion != null,
                Ref = newQuestion,
            };
        }

        [HttpPatch("{questionnaireID}/{questionID}")]
        public QuestionRsp Update(int questionnaireID, int questionID, QuestionReq request)
        {
            var questionnaire = QuestionnaireDataStore.Instance
                .GetQuestionnaire(questionnaireID);

            if (questionnaire == null)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionnaireExpiredOrInvalid,
                };

            var question = QuestionDataStore.Instance
                .GetQuestion(questionID);

            if (question == null)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionNotFoundInvalid,
                };

            var errors = new List<KeyValuePair<string, string>>();

            if (!request.Validate(errors))
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = errors,
                };

            question.Label = request.Label;
            question.Type = request.Type;
            question.ChoiceGroupID = request.ChoiceGroupID;
            question.IsMultiAnswer = request.IsMultiAnswer;

            return new QuestionRsp()
            {
                Success = QuestionDataStore.Instance
                    .Update(question),
                Ref = question,
            };
        }

        [HttpDelete("{questionnaireID}/{questionID}")]
        public QuestionRsp Delete(int questionnaireID, int questionID)
        {
            var questionnaire = QuestionnaireDataStore.Instance
                .GetQuestionnaire(questionnaireID);

            if (questionnaire == null)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionnaireExpiredOrInvalid,
                };

            var question = QuestionDataStore.Instance
                .GetQuestion(questionID);

            if (question == null)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionNotFoundInvalid,
                };

            return new QuestionRsp()
            {
                Success = QuestionDataStore.Instance
                    .Delete(question),
            };
        }

        [HttpPost("{questionnaireID}/swap/{questionID1}/{questionID2}")]
        public QuestionRsp SwapOrder(int questionnaireID, int questionID1, int questionID2)
        {
            var questionnaire = QuestionnaireDataStore.Instance
                .GetQuestionnaire(questionnaireID);

            if (questionnaire == null)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionnaireExpiredOrInvalid,
                };

            var question1 = QuestionDataStore.Instance
                .GetQuestion(questionID1);
            var question2 = QuestionDataStore.Instance
                .GetQuestion(questionID2);

            if (question1 == null ||
                question2 == null)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionNotFoundInvalid,
                };

            var question1Order = question1.DisplayOrder;
            var question2Order = question2.DisplayOrder;
            question1.DisplayOrder = question2Order;
            question2.DisplayOrder = question1Order;

            return new QuestionRsp()
            {
                Success =
                    QuestionDataStore.Instance
                        .Update(question1) &&
                    QuestionDataStore.Instance
                        .Update(question2),
            };
        }

        [HttpPost("{questionnaireID}/move-up/{questionID}")]
        public QuestionRsp MoveUpOrder(int questionnaireID, int questionID)
        {
            var questionnaire = QuestionnaireDataStore.Instance
                .GetQuestionnaire(questionnaireID);

            if (questionnaire == null)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionnaireExpiredOrInvalid,
                };

            var question = QuestionDataStore.Instance
                .GetQuestion(questionID);

            if (question == null)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionNotFoundInvalid,
                };

            var minDisplayOrder = questionnaire
                .GetMinDisplayOrder();

            if (question.DisplayOrder == minDisplayOrder)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = "Top order already.",
                };

            var previousQuestion = questionnaire.GetPreviousQuestion(question);

            return SwapOrder(questionnaire.ID, previousQuestion.ID, question.ID);
        }

        [HttpPost("{questionnaireID}/move-down/{questionID}")]
        public QuestionRsp MoveDownOrder(int questionnaireID, int questionID)
        {
            var questionnaire = QuestionnaireDataStore.Instance
                .GetQuestionnaire(questionnaireID);

            if (questionnaire == null)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionnaireExpiredOrInvalid,
                };

            var question = QuestionDataStore.Instance
                .GetQuestion(questionID);

            if (question == null)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = SystemMessage.QuestionNotFoundInvalid,
                };

            var maxDisplayOrder = questionnaire
                .GetMaxDisplayOrder();

            if (question.DisplayOrder == maxDisplayOrder)
                return new QuestionRsp()
                {
                    Success = false,
                    Ref = "Bottom order already.",
                };

            var nextQuestion = questionnaire.GetNextQuestion(question);

            return SwapOrder(questionnaire.ID, question.ID, nextQuestion.ID);
        }
    }
}
