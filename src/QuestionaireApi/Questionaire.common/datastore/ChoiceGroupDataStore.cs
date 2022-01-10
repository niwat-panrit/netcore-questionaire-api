using System;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class ChoiceGroupDataStore : DataStoreBase
    {
        public ChoiceGroupDataStore(DataStoreConfig config)
            : base(config)
        {
        }

        /// <summary>
        /// Get specific choice group from ID.
        /// </summary>
        /// <param name="groupID">The choice group identifier.</param>
        /// <returns>Choice group</returns>
        public ChoiceGroup GetGroup(int groupID)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<ChoiceGroup>()
                    .Where(c => c.ID == groupID)
                    .Take(1).SingleOrDefault();
        }

        /// <summary>
        /// Get specific choice group from name.
        /// </summary>
        /// <param name="groupName">Group name</param>
        /// <returns>Choice group</returns>
        public ChoiceGroup GetByName(string groupName)
        {
            using (var dbSession = OpenSession())
                return dbSession.QueryOver<ChoiceGroup>()
                    .Where(c => c.Name == groupName)
                    .Take(1).SingleOrDefault();
        }

        /// <summary>
        /// Save new choice group.
        /// </summary>
        /// <param name="choiceGroup">The new choice group</param>
        public void Create(ChoiceGroup choiceGroup)
        {
            using (var dbSession = OpenSession())
            {
                // TODO: Use DB's on create and update
                choiceGroup.CreatedAt =
                    choiceGroup.UpdatedAt =
                        DateTime.Now;

                dbSession.Save(choiceGroup);
            }
        }
    }
}
