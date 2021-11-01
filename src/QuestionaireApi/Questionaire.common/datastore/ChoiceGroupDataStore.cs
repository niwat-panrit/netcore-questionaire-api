﻿using System;
using Questionaire.common.model;

namespace Questionaire.common.datastore
{
    public class ChoiceGroupDataStore : DataStoreBase
    {
        // TODO: Optimize parameters

        private static ChoiceGroupDataStore _Instance;
        public static ChoiceGroupDataStore Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ChoiceGroupDataStore();

                return _Instance;
            }
        }

        public ChoiceGroupDataStore()
            : base()
        {
        }

        public ChoiceGroup GetGroup(int groupID)
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<ChoiceGroup>()
                    .Where(c => c.ID == groupID)
                    .Take(1).SingleOrDefault();
        }

        public ChoiceGroup GetByName(string groupName)
        {
            using (var dbSession = OpenStatelessSession())
                return dbSession.QueryOver<ChoiceGroup>()
                    .Where(c => c.Name == groupName)
                    .Take(1).SingleOrDefault();
        }

        public ChoiceGroup Create(ChoiceGroup choiceGroup)
        {
            using (var dbSession = OpenStatelessSession())
            {
                choiceGroup.CreatedAt =
                    choiceGroup.UpdatedAt =
                        DateTime.Now;
                choiceGroup.ID = (int)dbSession.Insert(choiceGroup);

                return choiceGroup;
            }
        }
    }
}
