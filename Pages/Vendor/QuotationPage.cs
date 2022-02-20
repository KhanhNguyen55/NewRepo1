using DebionTradePlatform.Commons;
using DebionTradePlatform.Services;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DebionTradePlatform.Pages.Vendor
{
    public class QuotationPage : PageService
    {
        readonly ElementService elementService;
        public QuotationPage(IWebDriver driver) : base(driver)
        {
            elementService = new ElementService();
        }

        #region
        // menu
        public IWebElement LeftmenuRequestForQuotation => Element("//div[text()='Request for quotation']");
        public IWebElement TextPageTitle => Element("//h5[text()='Request for quotation']");

        // In RFQ list
        public IWebElement RowRFQ(string RFQnumber) => Driver.FindElement(By.XPath($"//div[@class='MuiDataGrid-row' and .//a[text()='{RFQnumber}']]"));
        public IWebElement ButtonCreateQuotation => Element("//button[text()='Create quotation']");

        // In RFQ detail
        public IWebElement FieldUnitPrice => Element("//div[div[text()='Enter value']]");
        public IWebElement ButttonConfirm => Element("//button[text()='confirm']");
        public IWebElement PopupCreateSuccess => Element("//div[text()='Quotation created.']");
        public IWebElement ButtonRemoveQuotation => Element("//button[text()='Remove quotation']");
        public IWebElement ButtonCloseRFQDetail => Element("(//button[contains(@class,'RfqDetailDialog-closeButton')])[2]");
        #endregion

        public void OpenPage()
        {
            WaitService.WaitFor(() =>
            {
                Thread.Sleep(2000);
                LeftmenuRequestForQuotation.Click();
                return TextPageTitle.Displayed;
            }, 20).Should().BeTrue();
        }
        public void CreateQuotation(string RFQNumber)
        {
            WaitService.WaitFor(() =>
            {
                return RowRFQ(RFQNumber).Displayed;
            }, 10).Should().BeTrue();
            RowRFQ(RFQNumber).Click();

            ButtonCreateQuotation.Click();

            elementService.Input(FieldUnitPrice, "2500");
            WaitService.WaitFor(() =>
            {
                Thread.Sleep(3000);
                ButttonConfirm.Click();
                return ButtonRemoveQuotation.Displayed;
            }, 20).Should().BeTrue();
            PopupCreateSuccess.Displayed.Should().BeTrue();

            // close to open menu in next step
            ButtonCloseRFQDetail.Click();
        }
    }
}
