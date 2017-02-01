#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using GameTech.Elite.Reports;

namespace GameTech.Elite.Client.Modules.B3Center.Business
{
    public class B3Report
    {
        /// <summary>
        /// Gets or sets the ReportDocument that contains the Crystal Report.
        /// </summary>
        public ReportDocument CrystalReportDocument
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public ReportId Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the hash of the file of the report.
        /// </summary>
        public byte[] Hash
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Loads the crystal report.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="dbName">Name of the database.</param>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        public void LoadCrystalReport(string server, string dbName, string user, string password)
        {
            CrystalReportDocument = new ReportDocument();
            CrystalReportDocument.Load(FileName);
            SetConnection(CrystalReportDocument, server, dbName, user, password);
        }

        /// <summary>
        /// Sets the connection.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        private void SetConnection(ReportDocument report, string serverName, string databaseName, string userName, string password)
        {
            foreach (Table table in report.Database.Tables)
            {
                if (table.Name != "Command")
                {
                    SetTableConnectionInfo(table, databaseName, serverName, userName, password);
                }
            }

            foreach (ReportObject obj in report.ReportDefinition.ReportObjects)
            {
                if (obj.Kind != ReportObjectKind.SubreportObject)
                {
                    return;
                }

                var subReport = (SubreportObject)obj;
                var subReportDocument = report.OpenSubreport(subReport.SubreportName);
                SetConnection(subReportDocument, databaseName, serverName, userName, password);
            }
        }

        /// <summary>
        /// Sets the table connection information.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        private void SetTableConnectionInfo(Table table, string databaseName, string serverName, string userName, string password)
        {
            // Get the ConnectionInfo Object.
            var logOnInfo = table.LogOnInfo;
            var connectionInfo = logOnInfo.ConnectionInfo;

            // Set the Connection parameters.
            connectionInfo.DatabaseName = databaseName;
            connectionInfo.ServerName = serverName;
            connectionInfo.Password = password;
            connectionInfo.UserID = userName;
            table.ApplyLogOnInfo(logOnInfo);
        }
    }
}
