
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace csharp_selenium_lambdatest
{
    class SingleTest
    {
        public static void execute()
        {
            // Update your lambdatest credentials
            String LT_USERNAME = GetEnvironmentVariable("LT_USERNAME");
            String LT_ACCESS_KEY =  GetEnvironmentVariable("LT_ACCESS_KEY");
            IWebDriver driver;
            ChromeOptions capabilities = new ChromeOptions();
            capabilities.BrowserVersion = "latest";
            Dictionary<string, object> ltOptions = new Dictionary<string, object>();
            ltOptions.Add("username", LT_USERNAME);
            ltOptions.Add("accessKey", LT_ACCESS_KEY);
            ltOptions.Add("platformName", "Windows 10");
            ltOptions.Add("project", "Demo LT");
            ltOptions.Add("w3c", true);
            ltOptions.Add("plugin", "c#-c#");
            capabilities.AddAdditionalOption("LT:Options", ltOptions);

            driver = new RemoteWebDriver(new Uri("https://hub.lambdatest.com/wd/hub/"), capabilities);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                  Console.WriteLine("Navigating to todos app.");
                driver.Navigate().GoToUrl("https://lambdatest.github.io/sample-todo-app/");

                driver.FindElement(By.Name("li4")).Click();
                Console.WriteLine("Clicking Checkbox");
                driver.FindElement(By.Name("li5")).Click();


                // If both clicks worked, then te following List should have length 2
                IList<IWebElement> elems = driver.FindElements(By.ClassName("done-true"));
                // so we"ll assert that this is correct.
                Assert.AreEqual(2, elems.Count);

                Console.WriteLine("Entering Text");
                driver.FindElement(By.Id("sampletodotext")).SendKeys("Yey, Lets add it to list");
                driver.FindElement(By.Id("addbutton")).Click();


                // lets also assert that the new todo we added is in the list
                string spanText = driver.FindElement(By.XPath("/html/body/div/div/div/ul/li[6]/span")).Text;
                Assert.AreEqual("Yey, Lets add it to list", spanText);
                ((IJavaScriptExecutor)driver).ExecuteScript("lambda-status=passed");
            }
            catch
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("lambda-status=failed");
            }
            finally{  
            driver.Quit();
            }
        }
    }
}