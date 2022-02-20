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
    public class PurchaseOrderPage : PageService
    {
        readonly ElementService elementService;
        public PurchaseOrderPage(IWebDriver driver) : base(driver)
        {
            elementService = new ElementService();
        }

        #region
        // menu
        public IWebElement LeftmenuPurchaseOrder => Element("//div[text()='Purchase order']");
        public IWebElement TextPageTitle => Element("//h5[text()='Purchase Orders']");

        // In PO list
        public IWebElement RowPO(string PONumber) => Driver.FindElement(By.XPath($"//div[@class='MuiDataGrid-row' and .//a[text()='{PONumber}']]"));

        // In PO detail
        
        public IWebElement FieldDeliveryFee => Element("//input[@name='shippingFee']");
        public IWebElement ButtonConfirmPO => Element("//button[text()='Confirm PO']");
        public IWebElement ButtonViewInvoice => Element("//button[text()='View invoice']");

        // In PO confirm popup
        public IWebElement ButtonConfirmPOInPopup => Element("//button[text()='confirm']");
        public IWebElement ButtonCancelPOInPopup => Element("//button[text()='cancel']");
        public IWebElement PopupPOConfirmSuccess => Element("//div[text()='Purchase order confirmed!']");
        #endregion

        public void OpenPage()
        {
            WaitService.WaitFor(() =>
            {
                Thread.Sleep(1000);
                LeftmenuPurchaseOrder.Click();
                return TextPageTitle.Displayed;
            }, 10).Should().BeTrue();
        }
        public void ConfirmPOFromPOList(string PONumber, bool confirmPO = true)
        {
            WaitService.WaitFor(() =>
            {
                return RowPO(PONumber).Displayed;
            }, 10).Should().BeTrue();
            RowPO(PONumber).Click();

            elementService.Input(FieldDeliveryFee, "2500");
            ButtonConfirmPO.Click();

            if(confirmPO)
            {
                ButtonConfirmPOInPopup.Click();
                PopupPOConfirmSuccess.Displayed.Should().BeTrue();
            }
            else
            {
                ButtonCancelPOInPopup.Click();
            }
        }
        public void ConfirmPOFromPODetail(bool confirmPO = true)
        {
            elementService.Input(FieldDeliveryFee, "2500");
            ButtonConfirmPO.Click();

            if (confirmPO)
            {
                ButtonConfirmPOInPopup.Click();
                PopupPOConfirmSuccess.Displayed.Should().BeTrue();
            }
            else
            {
                ButtonCancelPOInPopup.Click();
            }
        }
    }
}
