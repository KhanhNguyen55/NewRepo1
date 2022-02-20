using DebionTradePlatform.Commons;
using DebionTradePlatform.Services;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DebionTradePlatform.Pages
{
    public class BuyerLoginPage : PageService
    {
        readonly BrowserService browserService;
        readonly ElementService elementService;
        public BuyerLoginPage(IWebDriver driver) : base(driver)
        {
            browserService = new BrowserService(driver);
            elementService = new ElementService();
        }
        
        //public BuyerLoginPage(BrowserService browser) : base(browser)
        //{
        //}

        #region
        public IWebElement FieldEmail => Element("//input[@name='email']");
        public IWebElement FieldPassword => Element("//input[@name='password']");
        public IWebElement ButtonSubmit => Element("//button[@type='submit']");
        #endregion

        public void Login(string email, string password, string url)
        {
            browserService.OpenURL(url);
            elementService.Input(FieldEmail, email);
            elementService.Input(FieldPassword, password);
            ButtonSubmit.Click();
        }
    }
}
