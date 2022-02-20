using DebionTradePlatform.Commons;
using DebionTradePlatform.Services;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebionTradePlatform.Pages.Buyer
{
    public class PurchaseOrderPage : PageService
    {
        public PurchaseOrderPage(IWebDriver driver) : base(driver)
        {
        }

        #region
        // In PO list
        public IWebElement RowPO(string PONumber) => Driver.FindElement(By.XPath($"//div[@class='MuiDataGrid-row' and .//a[text()='{PONumber}']]"));
        // In PO detail
        public IWebElement TextPONumber => Element("//div[h6[text()='PO number']]/p");
        public IWebElement ButtonViewInvoice => Element("//button[text()='View invoice']");
        public IWebElement ButtonClosePODetail => Element("//button[contains(@class,'PurchaseOrderDetailDialog-closeButton')]");
        
        #endregion

        public void OpenPONumberFromList(string PONumber)
        {
            WaitService.WaitFor(() =>
            {
                return RowPO(PONumber).Displayed;
            }, 20).Should().BeTrue();
            RowPO(PONumber).Click();
            RowPO(PONumber).Click();
        }
        public void OpenInvoiceFromeList(string PONumber)
        {
            WaitService.WaitFor(() =>
            {
                return RowPO(PONumber).Displayed;
            }, 20).Should().BeTrue();
            RowPO(PONumber).Click();
            ButtonViewInvoice.Click();
        }
    }
}
