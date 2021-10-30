using System;
using System.Collections.Generic;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class ChoiceDataStore : DataStoreBase
    {
        private static ChoiceDataStore _Instance;
        public static ChoiceDataStore Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ChoiceDataStore();

                return _Instance;
            }
        }

        public ChoiceDataStore()
            : base()
        {
        }

        public IList<Choice> GetByQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public IList<Choice> GetByGroup(int choiceGroupID)
        {
            throw new NotImplementedException();
        }

        public Choice Create(Choice choice)
        {
            throw new NotImplementedException();
        }

        public Choice GetChoice(int choiceID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Choice choice)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Choice choice)
        {
            throw new NotImplementedException();
        }
    }
}
