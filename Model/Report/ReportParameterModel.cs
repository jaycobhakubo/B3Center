using System;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Reports;
using GameTech.Elite.Client.Modules.B3Center.Model.Shared;

namespace GameTech.Elite.Client.Modules.B3Center.Model
{

    public class ReportParameterModel : ViewModelBase
    {
        public DatePickerM B3DateData {get;set;}
        public DatePickerM StartDate { get; set; }
        public DatePickerM EndDate { get; set; }
        public DateTime Date { get; set; }
        public string DateMonth { get; set; }
        public int DateYear { get; set; }
        public Session B3Session { get; set; }
        public string B3AccountNumber { get; set; }
        public string B3Category { get; set; }
        public string B3StartingCard { get; set; }
        public string B3EndingCard { get; set; }
        public ReportId Rptid { get; set; }
    }
}