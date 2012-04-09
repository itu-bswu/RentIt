?//-------------------------------------------------------------------------------------------------
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
    public class DataUtil : IDisposable
    {
        #region Fields

        /// <summary>
        /// EF Context.
        /// </summary>
        private readonly DbContext context;

        #endregion Fields

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="DataUtil"/> class.
        /// </summary>
        public DataUtil()
        {
            this.context = new RentItContext();
        }

        #endregion Constructor(s)

        #region Methods

        /// <summary>
        /// Empties all tables.
        /// </summary>
        public void Empty()
        {
            this.context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT all'");
            this.context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? DISABLE TRIGGER ALL'");
            this.context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'DELETE FROM ?'");
            this.context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'DBCC CHECKIDENT (''?'', RESEED, 0)'");
            this.context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'");
            this.context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? ENABLE TRIGGER ALL'");
        }

        /// <summary>
        /// Loads a given data-set.
        /// </summary>
        /// <param name="fileName">The SQL file to load.</param>
        public void Load(string fileName)
        {
            var sql = File.ReadAllText(Environment.CurrentDirectory + @"..\..\..\..\Test\Utils\" + fileName);
            var commands = sql.Split(new[] { "GO\r\n", "GO ", "GO\t" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var cmd in commands)
            {
                this.context.Database.ExecuteSqlCommand(cmd);
            }
        }

        #endregion Methods

        #region IDisposable

        /// <summary>
        /// Disposes the DataUtil.
        /// </summary>
        public void Dispose()
        {
            this.context.Dispose();
        }

        #endregion IDisposable
    }
}