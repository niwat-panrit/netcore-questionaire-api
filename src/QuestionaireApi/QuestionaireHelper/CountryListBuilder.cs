using System;
using System.IO;
using Questionaire.common.datastore;
using Questionaire.common.model;
using static QuestionaireHelper.Program;

namespace QuestionaireHelper
{
    class CountryListBuilder : ActionHandlerBase
    {
        private const string CountryGroupName = "COUNTRYLIST";

        public CountryListBuilder()
        {
            Logger?.Info($"Starting {nameof(CountryListBuilder)}...");
        }

        public override void Run(Options o)
        {
            Logger?.Info($"Building country list...");

            using (var dbSession = ChoiceDataStore.Instance.OpenSession())
            using (var transaction = dbSession.BeginTransaction())
            {
                try
                {
                    var countries = File.ReadAllLines(
                        Path.Combine(Program.Directory, "Data", "countries.csv"));
                    var countryGroup = ChoiceGroupDataStore.Instance
                        .GetByName(CountryGroupName);

                    if (countryGroup == null)
                        countryGroup = ChoiceGroupDataStore.Instance
                            .Create(new ChoiceGroup()
                            {
                                Name = CountryGroupName,
                                Description = "List of countries",
                            });
                    // TODO: Prevent duplicates

                    foreach (var name in countries)
                    {
                        var creationTime = DateTime.Now;
                        var choice = new Choice()
                        {
                            GroupID = countryGroup.ID,
                            Text = name,
                            CreatedAt = creationTime,
                            UpdatedAt = creationTime,
                        };
                        dbSession.Save(choice);
                    }
                    
                    transaction.Commit();
                    Logger?.Info($"Built successfully.");
                }
                catch (Exception exception)
                {
                    Logger?.Error($"Build failed.", exception);
                }
            }
        }
    }
}