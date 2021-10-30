using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Questionaire.common;
using Questionaire.common.datastore;
using Questionaire.common.model;

namespace QuestionaireApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionnaireController : ControllerBaseCustom
    {
        public QuestionnaireController()
            : base()
        {
        }

        [HttpGet("begin/{questionnaireID}")]
        public Session Begin(int questionnaireID) =>
            SessionDataStore.Instance
                .OpenQuestionnaireSession(questionnaireID);

        [HttpGet("get-question/{sessionID}")]
        [HttpGet("get-question/{sessionID}/{questionID}")]
        public QuestionRsp GetQuestion(int sessionID, int? questionID)
        {
            var session = SessionDataStore.Instance.GetSession(sessionID);

            if (session == null)
                return new QuestionRsp()
                {
                    Text = SystemMessage.SessionExpiredOrInvalid,
                };

            var response = new QuestionRsp()
            {
                Session = session,
            };

            if (response.Session.IsCompleted)
            {
                response.Text = SystemMessage.QuestionnaireCompleted;

                return response;
            }

            if (questionID == null)
                response.Question = response.Session
                        .GetFirstQuestion();
            else
                response.Question = QuestionDataStore.Instance
                    .GetQuestion((int)questionID);

            if (response.Question == null)
                response.Text = SystemMessage.QuestionExpiredOrInvalid;
            else if (response.Question.Type.Equals(QuestionType.Choice))
            {
                if (response.Question.ChoiceGroupID != null)
                    response.Choices = ChoiceDataStore.Instance
                        .GetByGroup((int)response.Question.ChoiceGroupID);
                else
                    response.Choices = ChoiceDataStore.Instance
                        .GetByQuestion(response.Question);
            }

            return response;
        }

        [HttpPost("submit-answers/{sessionID}/{questionID}")]
        public AnswerRsp SubmitAnswers(int sessionID, int questionID, AnswerReq[] answers)
        {
            try
            {
                var session = SessionDataStore.Instance
                    .GetSession(sessionID);

                if (session == null)
                    return new AnswerRsp()
                    {
                        IsAccepted = false,
                        Text = SystemMessage.SessionExpiredOrInvalid,
                    };

                var response = new AnswerRsp()
                {
                    Session = session,
                };
                var question = QuestionDataStore.Instance
                    .GetQuestion(questionID);

                if (question == null)
                {
                    response.IsAccepted = false;
                    response.Text = SystemMessage.QuestionIDInvalid;

                    return response;
                }
                
                var errors = new List<KeyValuePair<string, string>>();
                var success = answers.All(a => a.Validate(errors));

                if (!success)
                {
                    response.IsAccepted = false;
                    response.Text = JsonConvert.SerializeObject(errors);

                    return response;
                }

                var warnings = new List<string>();
                var isTerminatedByException = false;
                for (int i = 0; i < answers.Length; i++)
                {
                    var request = answers[i];
                    var newAnswer = new Answer()
                    {
                        QuestionnaireID = request.QuestionnaireID,
                        QuestionID = request.QuestionID,
                        SessionID = request.SessionID,
                        Value = request.Value,
                    };

                    if (!AnswerDataStore.Instance.Submit(session, question, newAnswer, i < answers.Length - 1, warnings, out bool isException))
                        warnings.Add($"Couldn't submit: {request}");

                    if (isException)
                        isTerminatedByException = true;
                }
                
                var nextQuestion = session.GetNextQuestion(question);
                var isLastQuestion = nextQuestion == null;
                var isSessionTerminated =
                    isTerminatedByException ||
                    isLastQuestion;
                
                if (isSessionTerminated)
                    session.Terminate();
                else
                    response.NextQuestionID = nextQuestion?.ID;

                if (warnings.Count > 0)
                    response.Text = JsonConvert.SerializeObject(warnings);

                return response;
            }
            catch (Exception exception)
            {
                this.logger?.Error(
                    $"Failed to save answers for session {sessionID} question {questionID}" + Environment.NewLine +
                        string.Join(Environment.NewLine, answers.ToList()),
                    exception);
                return new AnswerRsp()
                {
                    IsAccepted = false,
                    Text = SystemMessage.SomethingWrong,
                };
            }
        }
    }
}
