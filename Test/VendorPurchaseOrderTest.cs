using DebionTradePlatform.Commons;
using DebionTradePlatform.Pages.Vendor;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebionTradePlatform.Test
{
    public class VendorPurchaseOrderTest : TestService
    {
        readonly PurchaseOrderPage purchaseOrderPage;
        public VendorPurchaseOrderTest(IWebDriver driver) : base(driver)
        {
            purchaseOrderPage = new PurchaseOrderPage(driver);
        }

        public void VerifyConfirmPO(string PONumber)
        {
            purchaseOrderPage.OpenPage();
            //purchaseOrderPage.ConfirmPOFromPODetail();
            purchaseOrderPage.ConfirmPOFromPOList(PONumber);
            purchaseOrderPage.ButtonViewInvoice.Click();
        }
    }
}
