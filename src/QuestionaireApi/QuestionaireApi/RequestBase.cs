using System.Collections.Generic;

namespace QuestionaireApi
{
    public abstract class RequestBase
    {
        public abstract bool Validate(List<KeyValuePair<string, string>> errors);
    }
}
