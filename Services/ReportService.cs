using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebionTradePlatform.Services
{
    public class ReportService
    {
        public ReportService()
        {
        }

        public void CreateReport(string content)
        {
            File.WriteAllText(Constansts.Commons.Report.ReportPath, content);
        }
        public void AppendContent(string content)
        {
            File.AppendAllText(Constansts.Commons.Report.ReportPath, content + Environment.NewLine);
        }
        public string ReadReport()
        {
            string fileContent = File.ReadAllText(Constansts.Commons.Report.ReportPath);
            return fileContent;
        }
        public void ReplaceContent(string oldValue, string newValue)
        {
            string fileContent = File.ReadAllText(Constansts.Commons.Report.ReportPath);
            fileContent = fileContent.Replace(oldValue, newValue);
            File.WriteAllText(Constansts.Commons.Report.ReportPath, fileContent);
        }        
    }
}
