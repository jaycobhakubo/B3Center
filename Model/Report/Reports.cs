namespace GameTech.Elite.Client.Modules.B3Center.Model
{
    public class Reports
    {
        public string ReportName { get; set; }

        public int ReportId { get; set; }

        public string DisplayName
       {
           get { return string.Format("{0}", ReportName); }
       }

    }
   
}
