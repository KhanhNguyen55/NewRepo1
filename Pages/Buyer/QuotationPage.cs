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

namespace DebionTradePlatform.Pages.Buyer
{
    public class QuotationPage : PageService
    {
        readonly ElementService elementService;
        public QuotationPage(IWebDriver driver) : base(driver)
        {
            elementService = new ElementService();
        }

        //public QuotationPage(BrowserService browser) : base(browser)
        //{
        //}

        #region
        public IWebElement LeftmenuRequestForQuotation => Element("//div[text()='Request for quotation']");
        public IWebElement TextPageTitle => Element("//h5[text()='Request for quotation']");

        public IWebElement ButtonRequestForQuotation => Element("//button[text()='Request for quotation']");
        public IWebElement TextRFQNumber => Element("//h5/strong");

        public IWebElement FieldRFQNumber => Element("//input[@name='code']");
        public IWebElement DdlCity => Element("//input[@placeholder='Select province/city']");
        public IWebElement DdlDistrict => Element("//span[text()='District']");
        public IWebElement DdlWard => Element("//span[text()='Wards']");
        public IWebElement DdlAddress => Element("//input[@name='address']");
        public IWebElement DdlDueDate => Element("//input[@aria-label='Choose date']");

        public IWebElement DdlProductCategory => Element("(//div[contains(text(),'Select')])[1]");
        public IWebElement RadioOutdoorSideTables => Element("//div[text()='Outdoor Side Tables & Garden Stools']/span");
        public IWebElement ButtonOKInSelectCategory => Element("//button[contains(@class,'MuiButton-textPrimary')]");
        public IWebElement DdlUnit => Element("//div[contains(text(),'Select')]");
        public IWebElement DdlUnitOption(string name) => Driver.FindElement(By.XPath($"//li[@class = 'MuiAutocomplete-option' and text() = '{name}']"));
        public IWebElement AreaNewRow => Element("//div[@data-rowindex='1' and contains(@class,'creating')]");
        public IWebElement FieldSpecification => Element("//div[text()='Your desired specifications']");
        public IWebElement FieldFile => Element("//div[@class='DatagridDropzone-upLoadInput']");
        public IWebElement FieldQuantity => Element("//input[contains(@class,'MuiInputBase') and @value='1']");
        public IWebElement IconReset => Element("//button[@title='Reset']");
        public IWebElement ChboxVAT => Element("//input[contains(@class,'PrivateSwitchBase-input')]");
        public IWebElement FieldNote => Element("//input[contains(@name,'note')]");
        public IWebElement ButtonSendRequest => Element("//button[text()='Send request']");
        public IWebElement ButtonCloseRFQDetail => Element("(//button[contains(@class,'closeButton')])[2]");

        public IWebElement PopupSentRFQSuccess => Element("//div[contains(@class,'MuiDialogContent-root')]//h5");
        public IWebElement ButtonCloseInPopupSuccess => Element("//div[contains(@class,'MuiDialogActions-root')]/button");

        public IWebElement RowRFQ(string RFQnumber) => Driver.FindElement(By.XPath($"//div[@class='MuiDataGrid-row' and .//a[text()='{RFQnumber}']]"));
        public IWebElement AreaQuotation => Element("//div[p[contains(text(),'Total:')]]");
        public IWebElement ButtonConfirmAndCreatePO => Element("//button[text()='Confirm and create PO']");

        // area accept this quotation
        public IWebElement FieldCity => Element("//input[@placeholder='Select province/city']");
        public IWebElement FieldDistrict => Element("//input[@placeholder='Select district/district']");
        public IWebElement FieldWard => Element("//input[@placeholder='Select ward/commune']");
        public IWebElement FieldAddress => Element("//input[@name='address']");
        public IWebElement ButtonAccept => Element("//button[text()='accept']");

        public IWebElement ButtonCloseQuotationConfirm => Element("//button[text()='close']");
        public IWebElement ButtonReviewOrder => Element("//button[text()='Review order']");
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
        public string CreateCustomeRFQ()
        {
            ButtonRequestForQuotation.Click();
            string RFQNumber = TextRFQNumber.Text;

            // select value
            DdlProductCategory.Click();
            RadioOutdoorSideTables.Click();
            ButtonOKInSelectCategory.Click();

            DdlUnit.Click();
            WaitService.WaitFor(() =>
            {
                return DdlUnitOption("bộ").Displayed;
            }, 10).Should().BeTrue();
            DdlUnitOption("bộ").Click();

            AreaNewRow.Displayed.Should().BeTrue();
            ButtonSendRequest.Click();

            // verify result
            PopupSentRFQSuccess.Text.Should().Be("RFQ sent!");
            ButtonCloseInPopupSuccess.Click();

            ButtonCloseRFQDetail.Click();

            return RFQNumber;
        }
        public void CreatePO(string RFQNumber)
        {
            WaitService.WaitFor(() =>
            {
                return RowRFQ(RFQNumber).Displayed;
            }, 10).Should().BeTrue();
            RowRFQ(RFQNumber).Click();

            AreaQuotation.Click();
            ButtonConfirmAndCreatePO.Click();

            elementService.Input(FieldCity, "An");
            FieldCity.SendKeys(Keys.ArrowDown);
            FieldCity.SendKeys(Keys.Enter);
            elementService.InputAndSelectResult(FieldDistrict, "An");
            elementService.InputAndSelectResult(FieldWard, "An");
            elementService.Input(FieldAddress, "123 abc");
            ButtonAccept.Click();
        }
        public void ClickQuotationConfirmPopup(bool viewPO = true)
        {
            if(viewPO)
            {
                ButtonReviewOrder.Click();
            }
            else
            {
                ButtonCloseQuotationConfirm.Click();
            }
        }
    }
}
