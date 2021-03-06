using System;
using NHibernate.Tool.hbm2ddl;
using Questionaire.common.datastore;
using static QuestionaireHelper.Program;

namespace QuestionaireHelper
{
    class DBSchemaBuilder : ActionHandlerBase
    {
        public DBSchemaBuilder()
        {
            Logger?.Info($"Starting {nameof(DBSchemaBuilder)}...");
        }

        public override void Run(Options o)
        {
            while (true)
            {
                Console.WriteLine("Schema builder will clear existing tables then create new, confirm ? (y/n)");
                var confirm = Console.ReadLine();

                if (confirm.Equals("y", StringComparison.OrdinalIgnoreCase))
                    break;
                else if (confirm.Equals("n", StringComparison.OrdinalIgnoreCase))
                    return;
            }

            Logger?.Info($"Exporting DB schema...");

            var schemaExport = QuestionnaireDataStore.Instance
                .GetSchemaExport();
            var ddlFile = "hbm2schema.sql";
            schemaExport.SetOutputFile(ddlFile);
            schemaExport.Create(true, true);

            Logger?.Info($"DB schema exported to {ddlFile}.");
        }
    }
}
