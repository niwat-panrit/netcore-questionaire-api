using log4net;
using static QuestionaireHelper.Program;

namespace QuestionaireHelper
{
    abstract class ActionHandlerBase
    {
        protected static readonly ILog Logger = LogManager.GetLogger("App");

        public abstract void Run(Options option);
    }
}
