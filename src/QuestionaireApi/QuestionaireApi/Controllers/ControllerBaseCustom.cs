using log4net;
using Microsoft.AspNetCore.Mvc;

namespace QuestionaireApi.Controllers
{
    public abstract class ControllerBaseCustom : ControllerBase
    {
        protected ILog logger = LogManager.GetLogger("App");

        public ControllerBaseCustom()
        {
        }
    }
}
