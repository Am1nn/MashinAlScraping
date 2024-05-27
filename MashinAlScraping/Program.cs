using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
     
        var chromeDriverService = ChromeDriverService.CreateDefaultService();
        var chromeOptions = new ChromeOptions();

        
        using (var driver = new ChromeDriver(chromeDriverService, chromeOptions))
        {
            driver.Navigate().GoToUrl("https://mashin.al/qeydiyyat-nisanlari/");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            try
            {
                var gridItems = driver.FindElements(By.ClassName("registrationMarksGridItem"));

                foreach (var gridItem in gridItems)
                {
                    Thread.Sleep(500);

                    try
                    {
                        gridItem.Click();
                    }
                    catch {
                        continue;
                    }

                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


                    var windowHandles = driver.WindowHandles;

                    var currentWindowHandle = driver.CurrentWindowHandle;
                    if (windowHandles.Count > 1)
                    {

                        string newWindowHandle = null;
                        foreach (var handle in windowHandles)
                        {
                            if (handle != currentWindowHandle)
                            {
                                newWindowHandle = handle;
                                break;
                            }
                        }

                        driver.SwitchTo().Window(newWindowHandle);

                        string newUrl = driver.Url;
                        Console.WriteLine("Opening next tab with URL: " + newUrl);
                    }
                    
                    driver.Close();

                    driver.SwitchTo().Window(currentWindowHandle);

                }




            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Error:"+ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
