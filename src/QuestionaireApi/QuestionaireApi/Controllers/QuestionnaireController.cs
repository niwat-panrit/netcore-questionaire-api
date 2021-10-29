using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Questionaire.common.datastore;

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
        public SessionRsp Begin(int questionnaireID) => 
            new SessionRsp(SessionDataStore.GetNewSession(questionnaireID));

        [HttpGet("get-question/{sessionID}")]
        [HttpGet("get-question/{sessionID}/{questionID}")]
        public QuestionRsp GetQuestion(int sessionID, int? questionID)
        {
            var session = SessionDataStore.GetSession(sessionID);

            if (session == null)
                return new QuestionRsp()
                {
                    IsSessionTerminated = true,
                    Text = "Session is expired or invalid.",
                };

            if (session.IsCompleted)
                return new QuestionRsp()
                {
                    IsSessionTerminated = true,
                    Text = "Questionnaire is completed.",
                };

            if (questionID == null)
                return new QuestionRsp(QuestionDataStore.GetFirstQuestion(session));
            else
                return new QuestionRsp(QuestionDataStore.GetQuestion(session, (int)questionID));
        }

        [HttpPost("submit-answers/{sessionID}/{questionID}")]
        public AnswerRsp SubmitAnswers(int sessionID, int questionID, AnswerReq[] answers)
        {
            try
            {
                var session = SessionDataStore.GetSession(sessionID);

                if (session == null)
                    return new AnswerRsp()
                    {
                        IsAccepted = false,
                        IsSessionTerminated = true,
                        Text = "Session is expired or invalid.",
                    };

                var question = QuestionDataStore.GetQuestion(questionID);

                if (question == null)
                    return new AnswerRsp()
                    {
                        IsAccepted = false,
                        IsSessionTerminated = true,
                        Text = "Invalid question id.",
                    };

                var errors = new List<string>();
                answers.All(a => a.Validate(errors));

                if (errors.Count > 0)
                    return new AnswerRsp()
                    {
                        IsAccepted = false,
                        IsSessionTerminated = false,
                        Text = JsonConvert.SerializeObject(errors),
                    };

                var warnings = new List<string>();
                var isTerminatedByException = false;
                foreach (var answer in answers)
                {
                    if (!answer.Save(session, question, warnings, out bool isException))
                        warnings.Add($"Couldn't save: {answer}");

                    if (isException)
                        isTerminatedByException = true;
                }
                var isLastQuestion = session.IsLastQuestion(question);
                var isSessionTerminated =
                    isTerminatedByException ||
                    isLastQuestion;
                int? nextQuestionID = null;
                if (isSessionTerminated)
                    session.Terminate();
                else
                    nextQuestionID = session.GetNextQuestion(question);

                return new AnswerRsp()
                {
                    IsAccepted = true,
                    IsSessionTerminated = session.IsCompleted,
                    NextQuestionID = nextQuestionID,
                    Text = JsonConvert.SerializeObject(warnings),
                };
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
                    IsSessionTerminated = false, // TODO: Option to either retry or end on error
                    Text = "Something went wrong, please try again.",
                };
            }
        }
    }
}
