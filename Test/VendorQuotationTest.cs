using DebionTradePlatform.Commons;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebionTradePlatform.Test
{
    public class VendorQuotationTest : TestService
    {
        Pages.Vendor.QuotationPage vendorQuotationPage;
        public VendorQuotationTest(IWebDriver driver) : base(driver)
        {            
        }
        public override void Init()
        {
            vendorQuotationPage = new Pages.Vendor.QuotationPage(Driver);
        }

        //[Xunit.Fact]
        public void VerifyVendorCreateQuotation(string RFQNumber)
        {
            vendorQuotationPage.OpenPage();
            vendorQuotationPage.CreateQuotation(RFQNumber);
        }
    }
}
