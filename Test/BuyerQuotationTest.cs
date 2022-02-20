using DebionTradePlatform.Commons;
using DebionTradePlatform.Pages.Buyer;
using OpenQA.Selenium;

namespace DebionTradePlatform.Test
{
    public class BuyerQuotationTest : TestService
    {
        QuotationPage quotationPage;

        public BuyerQuotationTest() : base()
        {

        }
        public BuyerQuotationTest(IWebDriver driver) : base(driver)
        {

        }
        public override void Init()
        {
            quotationPage = new QuotationPage(Driver);
        }

        //[Xunit.Fact]
        public string VerifyBuyerCreateCustomRFQ()
        {
            quotationPage.OpenPage();
            string RFQNumber = quotationPage.CreateCustomeRFQ();
            return RFQNumber;
        }
        public void VerifyBuyerCreatePO(string RFQNumber)
        {
            quotationPage.OpenPage();
            quotationPage.CreatePO(RFQNumber);
            quotationPage.ClickQuotationConfirmPopup();
        }
    }
}
