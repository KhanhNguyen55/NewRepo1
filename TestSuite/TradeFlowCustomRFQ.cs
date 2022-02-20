using DebionTradePlatform.Commons;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Threading;

namespace DebionTradePlatform.TestSuite
{
    public class TradeFlowCustomRFQ : Commons.TestService
    {
        readonly Test.LoginTest buyerLoginTest, vendorLoginTest;
        readonly Services.BrowserService browserService, browserService2;
        readonly Test.BuyerQuotationTest buyerQuotationTest;
        readonly Test.VendorQuotationTest vendorQuotationTest;
        readonly Test.VendorPurchaseOrderTest vendorPurchaseOrderTest;
        readonly Pages.Commons.InvoicePage invoicePage;
        readonly Pages.Buyer.PurchaseOrderPage purchaseOrderPage;
        readonly Services.ReportService reportService;
        readonly Services.MessageService messageService;

        public TradeFlowCustomRFQ()
        {
            // buyer pages
            buyerLoginTest = new Test.LoginTest(Driver);
            buyerQuotationTest = new Test.BuyerQuotationTest(Driver);
            purchaseOrderPage = new Pages.Buyer.PurchaseOrderPage(Driver);

            // vendor pages
            browserService2 = new Services.BrowserService();
            vendorLoginTest = new Test.LoginTest(browserService2.Driver);
            vendorQuotationTest = new Test.VendorQuotationTest(browserService2.Driver);
            vendorPurchaseOrderTest = new Test.VendorPurchaseOrderTest(browserService2.Driver);

            // common pages
            invoicePage = new Pages.Commons.InvoicePage(browserService2.Driver);

            // services
            browserService = new Services.BrowserService(Driver);
            reportService = new Services.ReportService();
            messageService = new Services.MessageService();
        }

        [Xunit.Fact]
        [Xunit.Trait("type", "CustomeRFQ")]
        public void VerifyTradeFlowCustomRFQ()
        {
            // create file report
            DateTime dateTime = DateTime.UtcNow.Date;
            reportService.CreateReport("Result " + dateTime.ToString("dd/MM/yyyy") + ":\n");

            // buyer create RFQ
            buyerLoginTest.DoLogin();
            //string RFQNumber = buyerQuotationTest.VerifyBuyerCreateCustomRFQ();
            string RFQNumber = "";
            try
            {
                RFQNumber = buyerQuotationTest.VerifyBuyerCreateCustomRFQ();
            }
            catch
            {
                reportService.AppendContent("Buyer create RFQ failed!\n");
                throw;
            }
            //string RFQNumber = "#RFQ000677";

            // vendor create quota base on buyer RFQ
            vendorLoginTest.DoLogin("vendor");
            //vendorQuotationTest.VerifyVendorCreateQuotation(RFQNumber);
            try
            {
                vendorQuotationTest.VerifyVendorCreateQuotation(RFQNumber);
            }
            catch
            {
                reportService.AppendContent("Vendor create quota base on buyer RFQ failed!\n");
                throw;
            }

            // buyer accept quotation and create PO
            //buyerQuotationTest.VerifyBuyerCreatePO(RFQNumber);
            try
            {
                buyerQuotationTest.VerifyBuyerCreatePO(RFQNumber);
            }
            catch
            {
                reportService.AppendContent("Buyer create PO failed!\n");
                throw;
            }
            Thread.Sleep(3000);
            string PONumber = purchaseOrderPage.TextPONumber.Text;
            purchaseOrderPage.ButtonClosePODetail.Click();
            //string PONumber = "#PO000187";

            // vendor confirm buyer PO
            //vendorPurchaseOrderTest.VerifyConfirmPO(PONumber);
            try
            {
                vendorPurchaseOrderTest.VerifyConfirmPO(PONumber);
            }
            catch
            {
                reportService.AppendContent("Vendor confirm PO's buyer failed!\n");
                throw;
            }
            string vendorInvoiceNumber = invoicePage.TextInvoiceNo.Text;

            //buyer recheck invoice same as vender
            purchaseOrderPage.OpenInvoiceFromeList(PONumber);
            invoicePage.CheckInvoiceDetail(vendorInvoiceNumber);

            // close all browsers
            browserService.CloseBrowser();
            browserService2.CloseBrowser();
            reportService.AppendContent("Create PO from custome RFQ is OK!\n");

            // send message to telegram group
            string content = reportService.ReadReport();
            messageService.SendMessageToTelegram(content);
        }

        [Xunit.Fact]
        public void TestSendMessage()
        {
            string content = reportService.ReadReport();
            messageService.SendMessageToTelegram(content);

            // close all browsers
            browserService.CloseBrowser();
            browserService2.CloseBrowser();
        }
    }
}
