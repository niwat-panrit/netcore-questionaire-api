using System;
using System.Collections.Generic;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class ChoiceDataStore : DataStoreBase
    {
        // TODO: Optimize parameters

        public ChoiceDataStore(IDataStoreConfig config)
            : base(config)
        {
        }

        public IList<Choice> GetByQuestion(Question question)
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<Choice>()
                    .Where(c => c.QuestionID == question.ID)
                    .List();
        }

        public IList<Choice> GetByGroup(int choiceGroupID)
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<Choice>()
                    .Where(c => c.GroupID == choiceGroupID)
                    .List();
        }

        public Choice GetChoice(int choiceID)
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<Choice>()
                    .Where(c => c.ID == choiceID)
                    .Take(1).SingleOrDefault();
        }

        public Choice Create(Choice choice)
        {
            using (var dbSession = OpenStatelessSession())
            {
                choice.CreatedAt = 
                    choice.UpdatedAt =
                        DateTime.Now;
                choice.ID = (int)dbSession.Insert(choice);

                return choice;
            }
        }

        public bool Update(Choice choice)
        {
            using (var dbSession = OpenSession())
            {
                choice.UpdatedAt = DateTime.Now;
                dbSession.Update(choice);

                return true;
            }
        }

        public bool Delete(Choice choice)
        {
            using (var dbSession = OpenSession())
            {
                dbSession.Delete(choice);
                
                return true;
            }
        }
    }
}
