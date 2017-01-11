using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTech.Elite.Client.Modules.B3Center.Model;

namespace GameTech.Elite.Client.Modules.B3Center.ViewModels.Reports
{
    public class ballcallVm : ReportTemplateViewModel
    {
        public ballcallVm(ReportTemplateModel reportTemplateModel) : base(reportTemplateModel)
        {
            bcvm = this;
        }

        public ReportTemplateViewModel bcvm
        {
            get;set;
        } 

    }
}
