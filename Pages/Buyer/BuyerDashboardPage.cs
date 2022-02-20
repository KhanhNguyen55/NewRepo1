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

namespace DebionTradePlatform.Pages
{
    public class BuyerDashboardPage: PageService
    {
        public BuyerDashboardPage(IWebDriver driver) : base(driver)
        {
        }

        //public BuyerDashboardPage(BrowserService browser) : base(browser)
        //{
        //}

        #region
        public IWebElement ButtonAvatar => Element("//div[contains(@class,'MuiAvatar')]");
        public IWebElement TextEmail => Element("//p[contains(@class,'MuiTypography')]");
        public IWebElement TextLoginSuccessed => Element("//div[@id='notistack-snackbar']");
        
        #endregion

        public void VerifyLoginSuccess()
        {
            TextLoginSuccessed.Displayed.Should().BeTrue();
        }
    }
}
