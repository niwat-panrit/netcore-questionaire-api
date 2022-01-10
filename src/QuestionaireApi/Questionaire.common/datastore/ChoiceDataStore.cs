using System;
using System.Collections.Generic;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    // Notes:
    //  Created object's ID will get assigned after inserted: https://stackoverflow.com/questions/925923/using-nhibernate-how-can-i-get-the-newly-inserted-id-during-an-add
    //  Trust NHibernate to perform and verify update and delete: https://stackoverflow.com/questions/12369031/affected-rows-with-save-delete-saveorupdate-in-nhibernate
    public class ChoiceDataStore : DataStoreBase
    {
        public ChoiceDataStore(DataStoreConfig config)
            : base(config)
        {
        }

        /// <summary>
        /// Get lsit of choices for specified question.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns>lsit of choices</returns>
        public IList<Choice> GetByQuestion(Question question)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Choice>()
                    .Where(c => c.QuestionID == question.ID)
                    .List();
        }

        /// <summary>
        /// Get lsit of choices for specified choice group.
        /// </summary>
        /// <param name="ChoiceGroup">The choice group.</param>
        /// <returns>lsit of choices</returns>
        public IList<Choice> GetByGroup(ChoiceGroup choiceGroup)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Choice>()
                    .Where(c => c.GroupID == choiceGroup.ID)
                    .List();
        }

        /// <summary>
        /// Get specific choice from ID.
        /// </summary>
        /// <param name="choiceID">The choice identifier.</param>
        /// <returns>Choice</returns>
        public Choice GetChoice(int choiceID)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<Choice>()
                    .Where(c => c.ID == choiceID)
                    .Take(1).SingleOrDefault();
        }

        /// <summary>
        /// Save new choice.
        /// </summary>
        /// <param name="choice">The new choice.</param>
        public void Create(Choice choice)
        {
            using (var dbSession = OpenSession())
            {
                // TODO: Use DB's on create and update
                choice.CreatedAt = 
                    choice.UpdatedAt =
                        DateTime.Now;

                dbSession.Save(choice);
            }
        }

        /// <summary>
        /// Update existing choice.
        /// </summary>
        /// <param name="choice">The updating choice</param>
        public void Update(Choice choice)
        {
            using (var dbSession = OpenSession())
            {
                // TODO: Use DB's on create and update
                choice.UpdatedAt = DateTime.Now;

                dbSession.Update(choice);
            }
        }

        /// <summary>
        /// Delete existing choice.
        /// </summary>
        /// <param name="choice">The deleting choice.</param>
        public void Delete(Choice choice)
        {
            using (var dbSession = OpenSession())
            {
                dbSession.Delete(choice);
            }
        }
    }
}
