using DebionTradePlatform.Commons;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebionTradePlatform.Pages.Commons
{
    public class InvoicePage : PageService
    {
        public InvoicePage(IWebDriver driver) : base(driver)
        {
        }

        #region
        public IWebElement TextInvoiceNo => Element("//p[contains(text(),'Invoice No')]");
        #endregion

        public void CheckInvoiceDetail(string PONumber)
        {
            TextInvoiceNo.Text.Should().Be(PONumber);
        }
    }
}
