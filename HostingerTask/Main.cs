using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace HostingerTask
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            IWebDriver driver = new ChromeDriver();
            Actions actions = new Actions(driver);
            driver.Navigate().GoToUrl("https://www.hostinger.com");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            var cookieButton = driver.FindElement(By.CssSelector("button[data-click-id='hgr-cookie_consent-accept_all_btn']"));//finding accept cookie button
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(a => cookieButton.Displayed);//waiting for cookieButton to appear
            wait.IgnoreExceptionTypes();
            cookieButton.Click();
            var addToCart = driver.FindElement(By.CssSelector("button[data-click-id='hgr-homepage-pricing_table-add_to_cart-hosting_hostinger_business']"));
            //addToCart button would not be clickable right after it is loaded, so it is better to scroll down to it and then click it         
            actions.MoveToElement(addToCart).Perform();
            addToCart.Click();
            //-------------Checkout Login Section------------------
            driver.FindElement(By.Id("hcart-login-secondary")).Click();//log in button
            driver.FindElement(By.XPath("(//input[@type='text'])")).SendKeys("for-candidates-2024-02-28@hostinger.com");//username
            driver.FindElement(By.XPath("(//input[@type='password'])")).SendKeys("Candidate!22" + Keys.Return);//psw
            //-----------Log out checker for loading-----------
            var logOut = driver.FindElement(By.Id("hcart-logout"));
            wait.Until(a => logOut.Displayed);
            wait.IgnoreExceptionTypes();
            //--------------------------------------------
            driver.FindElement(By.XPath("(//span[normalize-space()='24 months'])")).Click();//24months option
            var check24month = driver.FindElement(By.XPath("//h4[normalize-space()='Business Web Hosting - 24 Months Plan']"));
            wait.Until(b => check24month.Enabled); //checking if the bottom form is updated after changing plan
            driver.FindElement(By.Id("hcart-payment-method-select-checkout_googlepay")).Click();//googlepay payment selection
            Thread.Sleep(TimeSpan.FromSeconds(2));

            //------------Form----------------------------------
            driver.FindElement(By.XPath("(//div[@id='first-name-input']//input[@type='text'])")).SendKeys("Test1");
            driver.FindElement(By.XPath("(//div[@id='last-name-input']//input[@type='text'])")).SendKeys("Test2");
            driver.FindElement(By.XPath("(//div[@id='phone-number-input']//input[@type='text'])")).SendKeys("000000000");
            driver.FindElement(By.XPath("(//div[@id='region-input']//input[@type='text'])")).SendKeys("Region");
            driver.FindElement(By.XPath("(//div[@id='city-input']//input[@type='text'])")).SendKeys("City");
            driver.FindElement(By.XPath("(//div[@id='address-input']//input[@type='text'])")).SendKeys("Address");
            driver.FindElement(By.XPath("(//div[@id='zip-input']//input[@type='text'])")).SendKeys("12345");

            //-------------submit payment-----------------
            driver.FindElement(By.Id("hcart-submit-payment")).Click();//Submit Secure Payment button
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.ClassName("gpay-card-info-placeholder-container")).Click();//Google Pay submit button
        }
    }
}