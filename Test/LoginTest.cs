using DebionTradePlatform.Commons;
using DebionTradePlatform.Pages;
using OpenQA.Selenium;
using Xunit;

namespace DebionTradePlatform.Test
{
    public class LoginTest : TestService
    {
        readonly BuyerLoginPage buyerLoginPage;
        readonly BuyerDashboardPage buyerDashboardPage;

        public LoginTest() : base()
        {
            buyerLoginPage = new BuyerLoginPage(Driver);
            buyerDashboardPage = new BuyerDashboardPage(Driver);
        }
        public LoginTest(IWebDriver driver) : base(driver)
        {
            buyerLoginPage = new BuyerLoginPage(Driver);
            buyerDashboardPage = new BuyerDashboardPage(Driver);
        }

        public void DoLogin(string type = "buyer")
        {
            switch (type)
            {
                case "buyer":
                    buyerLoginPage.Login(Constansts.Accounts.buyerEmail, Constansts.Accounts.buyerPassword, Constansts.Accounts.BuyerLoginURL);
                    break;
                case "vendor":
                    buyerLoginPage.Login(Constansts.Accounts.vendorEmail, Constansts.Accounts.vendorPassword, Constansts.Accounts.VendorLoginURL);
                    break;
            }
            buyerDashboardPage.VerifyLoginSuccess();
        }
    }
}
