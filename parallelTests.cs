 using System.Threading;
    using System;
    using static System.Environment;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Safari;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Support.UI;
    
    namespace csharp_selenium_lambdatest
    {
        class ParallelTests
        {
            public static void execute()
            {
                Thread combination1 = new Thread(obj => sampleTestCase("Safari", "latest", "MacOS Ventura"));
                Thread combination2 = new Thread(obj => sampleTestCase("Chrome", "latest", "Windows 10"));
                Thread combination3 = new Thread(obj => sampleTestCase("Firefox", "latest", "MacOS Monterey"));
                Thread combination4 = new Thread(obj => sampleTestCase("Safari", "latest", "MacOS Big Sur"));
                Thread combination5 = new Thread(obj => sampleTestCase("Edge", "latest", "Windows 10"));

                //Executing the methods
                combination1.Start();
                combination2.Start();
                combination3.Start();
                combination4.Start();
                combination5.Start();
                combination1.Join();
                combination2.Join();
                combination3.Join();
                combination4.Join();
                combination5.Join();
            }

            static void sampleTestCase(String browser, String browser_version, String platform)
            {
                // Update your lambdatest credentials
                String LT_USERNAME = GetEnvironmentVariable("LT_USERNAME");
                String LT_ACCESS_KEY = GetEnvironmentVariable("LT_ACCESS_KEY");
                switch (browser)
                {
                    case "Safari": 
                    //If browser is Safari, following capabilities will be passed to 'executetestwithcaps' function
                        SafariOptions safari = new SafariOptions();
                        safari.BrowserVersion = browser_version;
                        Dictionary<string, object> safariltOptions = new Dictionary<string, object>();
                        safariltOptions.Add("username",LT_USERNAME );
                        safariltOptions.Add("accessKey", LT_ACCESS_KEY);
                        safariltOptions.Add("platformName", platform);
                        safariltOptions.Add("project", "Demo LT");
                        safariltOptions.Add("build", "C# Build");
                        safariltOptions.Add("sessionName", "C# Parallel Test");
                        safariltOptions.Add("w3c", true);
                        safariltOptions.Add("plugin", "c#-c#");
                        safari.AddAdditionalOption("LT:Options", safariltOptions);
                        executetestwithcaps(safari);
                        break;
                    case "Chrome" : //If browser is Chrome, following capabilities will be passed to 'executetestwithcaps' function
                        ChromeOptions chrome = new ChromeOptions();
                        chrome.BrowserVersion = browser_version;
                        Dictionary<string, object> chromeltOptions = new Dictionary<string, object>();
                        chromeltOptions.Add("username",LT_USERNAME );
                        chromeltOptions.Add("accessKey", LT_ACCESS_KEY);
                        chromeltOptions.Add("platformName", platform);
                        chromeltOptions.Add("project", "Demo LT");
                        chromeltOptions.Add("build", "C# Build");
                        chromeltOptions.Add("sessionName", "C# Parallel Test");
                        chromeltOptions.Add("w3c", true);
                        chromeltOptions.Add("plugin", "c#-c#");
                        chrome.AddAdditionalOption("LT:Options", chromeltOptions);
                        executetestwithcaps(chrome);
                        break;
                    case "Firefox": //If browser is Firefox, following capabilities will be passed to 'executetestwithcaps' function
                        FirefoxOptions firefox = new FirefoxOptions();
                        firefox.BrowserVersion = browser_version;
                        Dictionary<string, object> firefoxltOptions = new Dictionary<string, object>();
                        firefoxltOptions.Add("username",LT_USERNAME );
                        firefoxltOptions.Add("accessKey", LT_ACCESS_KEY);
                        firefoxltOptions.Add("platformName", platform);
                        firefoxltOptions.Add("project", "Demo LT");
                        firefoxltOptions.Add("build", "C# Build");
                        firefoxltOptions.Add("sessionName", "C# Parallel Test");
                        firefoxltOptions.Add("w3c", true);
                        firefoxltOptions.Add("plugin", "c#-c#");
                        firefox.AddAdditionalOption("LT:Options", firefoxltOptions);
                        executetestwithcaps(firefox);
                        break;
                    case "Edge": //If browser is Edge, following capabilities will be passed to 'executetestwithcaps' function
                        EdgeOptions edge = new EdgeOptions();
                        edge.BrowserVersion = browser_version;
                        Dictionary<string, object> edgeltOptions = new Dictionary<string, object>();
                        edgeltOptions.Add("username",LT_USERNAME );
                        edgeltOptions.Add("accessKey", LT_ACCESS_KEY);
                        edgeltOptions.Add("platformName", platform);
                        edgeltOptions.Add("project", "Demo LT");
                        edgeltOptions.Add("build", "C# Build");
                        edgeltOptions.Add("sessionName", "C# Parallel Test");
                        edgeltOptions.Add("w3c", true);
                        edgeltOptions.Add("plugin", "c#-c#");
                        edge.AddAdditionalOption("LT:Options", edgeltOptions);
                        executetestwithcaps(edge);
                        break;
                    default: //If browser is IE, following capabilities will be passed to 'executetestwithcaps' function
                        ChromeOptions capabilities = new ChromeOptions();
                       capabilities.BrowserVersion = browser_version;
                        Dictionary<string, object> ltOptions = new Dictionary<string, object>();
                        ltOptions.Add("username",LT_USERNAME );
                        ltOptions.Add("accessKey", LT_ACCESS_KEY);
                        ltOptions.Add("platformName", platform);
                        ltOptions.Add("project", "Demo LT");
                        ltOptions.Add("build", "C# Build");
                        ltOptions.Add("sessionName", "C# Parallel Test");
                        ltOptions.Add("w3c", true);
                        ltOptions.Add("plugin", "c#-c#");
                        capabilities.AddAdditionalOption("LT:Options", ltOptions);
                        executetestwithcaps(capabilities);
                        break;
                }
            }
            //executetestwithcaps function takes capabilities from 'sampleTestCase' function and executes the test
            static void executetestwithcaps(DriverOptions capabilities)
            {
                IWebDriver driver = new RemoteWebDriver(new Uri("https://hub.lambdatest.com/wd/hub/"), capabilities);
                 try
                {
                      Console.WriteLine("Navigating to todos app.");
                    driver.Navigate().GoToUrl("https://lambdatest.github.io/sample-todo-app/");

                    driver.FindElement(By.Name("li4")).Click();
                    Console.WriteLine("Clicking Checkbox");
                    driver.FindElement(By.Name("li5")).Click();

                    // If both clicks worked, then te following List should have length 2
                    IList<IWebElement> elems = driver.FindElements(By.ClassName("done-true"));
                    // so we'll assert that this is correct.
                    if ( elems.Count != 2)
                    throw new Exception();
                    
                    Console.WriteLine("Entering Text");
                    driver.FindElement(By.Id("sampletodotext")).SendKeys("Yey, Let's add it to list");
                    driver.FindElement(By.Id("addbutton")).Click();

                    // lets also assert that the new todo we added is in the list
                    string spanText = driver.FindElement(By.XPath("/html/body/div/div/div/ul/li[6]/span")).Text;
                  if (!"Yey, Let's add it to list".Equals(spanText))
                    throw new Exception();
               
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
    }}