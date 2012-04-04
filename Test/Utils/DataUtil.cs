//-------------------------------------------------------------------------------------------------
// <copyright file="DataUtil.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Utils
{
    using System;
    using System.Data.Entity;
    using System.IO;
    using RentItService;

    /// <summary>
    /// Utility for loading data.
    /// </summary>
    public static class DataUtil
    {
        #region Fields

        /// <summary>
        /// Database connection.
        /// </summary>
        private static readonly Database Database;

        #endregion Fields

        #region Constructor(s)

        /// <summary>
        /// Initializes static members of the <see cref="DataUtil"/> class.
        /// </summary>
        static DataUtil()
        {
            var context = new RentItContext();
            Database = context.Database;
        }

        #endregion Constructor(s)

        #region Methods

        /// <summary>
        /// Empties all tables.
        /// </summary>
        public static void Empty()
        {
            Database.ExecuteSqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'");
            Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? DISABLE TRIGGER ALL'");
            Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'DELETE FROM ?'");
            Database.ExecuteSqlCommand("exec sp_MSforeachtable 'DBCC CHECKIDENT (''?'', RESEED)'");
            Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'");
            Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? ENABLE TRIGGER ALL'");
        }

        /// <summary>
        /// Loads a given data-set.
        /// </summary>
        /// <param name="fileName">The SQL file to load.</param>
        public static void Load(string fileName)
        {
            var sql = File.ReadAllText(Environment.CurrentDirectory + @"..\..\..\..\Test\Utils\" + fileName);
            var commands = sql.Split(new[] { "GO\r\n", "GO ", "GO\t" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var cmd in commands)
            {
                Database.ExecuteSqlCommand(cmd);
            }
        }

        #endregion Methods
    }
}
