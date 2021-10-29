using System;
using System.Collections.Generic;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class ChoiceDataStore
    {
        public ChoiceDataStore()
        {
        }

        public static IList<Choice> GetByQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public static IList<Choice> GetByGroup(int choiceGroupID)
        {
            throw new NotImplementedException();
        }
    }
}
