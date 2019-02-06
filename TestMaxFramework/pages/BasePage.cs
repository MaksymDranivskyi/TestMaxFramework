using System;
using System.IO;
using System.Threading;
using System.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using Serilog;
using Serilog.Events;
using TestMaxFramework.pages;

namespace TestMaxFramework.pages
{
    public class BasePage
    {
        public string BASE_URL = "https://www.phptravels.net";
        public string pageURL = "";
        public string pageTitle = "";

        public static ThreadLocal<IWebDriver> thread = new ThreadLocal<IWebDriver>();
        public static IWebDriver driver() { return thread.Value; }

        public static int DEFAULT_TIMEOUT = getTimeout();
        public static int SHORT_TIMEOUT = getShortTimeout();
        public static int STATIC_TIMEOUT = getStaticTimeout();

        public static string
        mouseOverScript = "if(document.createEvent){var evObj = document.createEvent('MouseEvents');evObj.initEvent('mouseover',true, false); " +
            "arguments[0].dispatchEvent(evObj);} else if(document.createEventObject) { arguments[0].fireEvent('onmouseover');}",
        mouseOverScript2 = "var evObj = document.createEvent('MouseEvents'); " +
            "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null); arguments[0].dispatchEvent(evObj);",
        clickScript = "arguments[0].click();",
        DragNdropScript = "var ball = document.getElementById('ball'); ball.style.position = 'absolute'; moveAt(e); ";


        private static int getTimeout()
        {
            Log.Debug("Gets the timeout");
            string timeout = "15";
            if (timeout == null)
            {
                Console.WriteLine("DefaultTimeoutInSeconds parameter was not found");
                timeout = "15";
            };

            return Int32.Parse(timeout);
        }


        private static int getShortTimeout()
        {
            Log.Debug("Gets the short timeout");
            string timeout = "3";
            if (timeout == null)
            {
                timeout = "3";
            };

            return Int32.Parse(timeout);
        }


        private static int getStaticTimeout()
        {
            Log.Debug("Gets the static timeout");
            string timeout = "3";
            if (timeout == null)
            {
                timeout = "1000";
            };
            return Int32.Parse(timeout);
        }



        /*--------------------------------------------------------------------------*
         *----------------------------Basic assertion---------------------------------*
         *--------------------------------------------------------------------------*/

        public bool isPageLoaded()
        {
            Log.Debug("Check if the page is loaded");
            bool result = false;
            if (driver().Title.Contains(pageTitle))
                result = true;
            else
            {
                result = false;
            }

            if (driver().Url.Contains(pageURL))
                result = true;
            else
            {
                result = false;
            }
            return result;
        }


        public bool isElementPresentAndDisplay(By by)
        {
            Log.Debug("Check if element is present and display");
            try
            {
                return findElementIgnoreException(by).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
            
        }



        /*--------------------------------------------------------------------------*
         *----------------------------Basic actions---------------------------------*
         *--------------------------------------------------------------------------*/

        public void open()
        {
            Log.Debug($"Open this instance {BASE_URL}{pageURL}");
            driver().Url = DriverProvider.baseURL + pageURL;
        }

      


       
        public static void hoverAndClick(By toHover, By toClick, params int[] timeout)
        {
            Log.Debug("Hovers the first element and clicks second");
            int timeoutForFindElement = timeout.Length < 1 ? DEFAULT_TIMEOUT : timeout[0];
            waitForPageToLoad();
            try
            {
                ((IJavaScriptExecutor)driver()).ExecuteScript(mouseOverScript, driver().FindElement(toHover));
                ((IJavaScriptExecutor)driver()).ExecuteScript(clickScript, driver().FindElement(toClick));
            }
            catch (Exception)
            {
                hoverAndClick(toHover, toClick);
            }
            waitForPageToLoad();
        }


        public static void hoverAndClick(By toHover1, By toHover2, By toClick, params int[] timeout)
        {
            Log.Debug("Hovers the and click");
            int timeoutForFindElement = timeout.Length < 1 ? DEFAULT_TIMEOUT : timeout[0];
            waitForPageToLoad();
            try
            {
                ((IJavaScriptExecutor)driver()).ExecuteScript(mouseOverScript, driver().FindElement(toHover1));
                ((IJavaScriptExecutor)driver()).ExecuteScript(mouseOverScript, driver().FindElement(toHover2));
                ((IJavaScriptExecutor)driver()).ExecuteScript(clickScript, driver().FindElement(toClick));
            }
            catch (Exception)
            {
                hoverAndClick(toHover1, toHover2, toClick);
            }
        }


        public static By hover(By element, params int[] timeout)
        {
            Log.Debug("Hover the specified element");
            int timeoutForFindElement = timeout.Length < 1 ? DEFAULT_TIMEOUT : timeout[0];
            waitForPageToLoad();
            try
            {
                (new WebDriverWait(driver(), TimeSpan.FromSeconds(timeoutForFindElement)))
                    .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
                ((IJavaScriptExecutor)driver()).ExecuteScript(mouseOverScript, driver().FindElement(element));
            }
            catch (Exception)
            { 
                throw new Exception("Failure hovering on element");
            }
            waitForPageToLoad();
            return element;
        }

 
        public static By dragNdrop(By element, params int[] timeout)
        {
            Log.Debug("Drags and drop specified element");
            int timeoutForFindElement = timeout.Length < 1 ? DEFAULT_TIMEOUT : timeout[0];
            waitForPageToLoad();
            try
            {
                (new WebDriverWait(driver(), TimeSpan.FromSeconds(timeoutForFindElement)))
                    .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
                ((IJavaScriptExecutor)driver()).ExecuteScript(DragNdropScript, driver().FindElement(element));
            }
            catch (Exception)
            {
                throw new Exception("Failure dragging the element");
            }
            waitForPageToLoad();
            return element;
        }


        public static void waitForPageToLoad()
        {
            Log.Debug("Waits for page to load");
            bool pageIsLoaded = ((IJavaScriptExecutor)driver()).ExecuteScript("return document.readyState").Equals("complete");
            IWait<IWebDriver> wait = new WebDriverWait(driver(), TimeSpan.FromSeconds(DEFAULT_TIMEOUT));
        }


        public static void sleepFor(int timeout)
        {
            Log.Debug($"Thread sleep {timeout}");
            try
            {
                Thread.Sleep(timeout);
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        public static IWebElement findElementIgnoreException(By element, params int[] timeout)
        {
            Log.Debug("Finds the element ignore exception");
            waitForPageToLoad();
            int timeoutForFindElement = timeout.Length < 1 ? DEFAULT_TIMEOUT : timeout[0];
            waitForPageToLoad();
            try
            {

                (new WebDriverWait(driver(), TimeSpan.FromSeconds(timeoutForFindElement)))
                        .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
                return driver().FindElement(element);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static void clickOnElementIgnoreException(By element, params int[] timeout)
        {
            Log.Debug("Clicks the element ignore exception");
            waitForPageToLoad();
            int timeoutForFindElement = timeout.Length < 1 ? DEFAULT_TIMEOUT : timeout[0];
            try
            {
                (new WebDriverWait(driver(), TimeSpan.FromSeconds(timeoutForFindElement)))
                        .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
                driver().FindElement(element).Click();
            }
            catch (Exception)
            {
                Log.Information("error");
            }
            waitForPageToLoad();
        }


        public static void selectByIndex(By element, int value, params int[] timeout)
        {
            Log.Debug($"Select element {element} by index {value}");
            waitForPageToLoad();
            int timeoutForFindElement = timeout.Length < 1 ? DEFAULT_TIMEOUT : timeout[0];
            try
            {
                (new WebDriverWait(driver(), TimeSpan.FromSeconds(timeoutForFindElement)))
                        .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
                IWebElement passangersElement = findElement(element);
                SelectElement passangersSelect = new SelectElement(passangersElement);
                passangersSelect.SelectByIndex(value);
            }
            catch (Exception)
            {
                Log.Information("Select element exception");
            } 
        }

        public static void selectByString(By element, string value, params int[] timeout)
        {
            Log.Debug($"Select element {element} by string {value}");
            waitForPageToLoad();
            int timeoutForFindElement = timeout.Length < 1 ? DEFAULT_TIMEOUT : timeout[0];
            try
            {
                (new WebDriverWait(driver(), TimeSpan.FromSeconds(timeoutForFindElement)))
                        .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
                IWebElement passangersElement = findElement(element);
                SelectElement passangersSelect = new SelectElement(passangersElement);
                sleepFor(600);
                passangersSelect.SelectByValue(value);
            }
            catch (Exception)
            {
                Log.Information("Select element exception");
            }
        }

        public static void selectByValue(By element, string value, params int[] timeout)
        {
            Log.Debug($"Select dropdown item {element} by three letters {value}");
            waitForPageToLoad();
            int timeoutForFindElement = timeout.Length < 1 ? DEFAULT_TIMEOUT : timeout[0];
            try
            {
                (new WebDriverWait(driver(), TimeSpan.FromSeconds(timeoutForFindElement)))
                        .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
                clickOnElement(element);
                driver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutForFindElement);
                findElement(By.ClassName("select2-focused")).SendKeys(value);
                sleepFor(600);
                findElement(By.ClassName("select2-focused")).SendKeys(Keys.Enter);
            }
            catch (Exception)
            {
                Log.Information("Select element exception");
            }

        }
      


        public static IWebElement findElement(By element, params int[] timeout)
        {
            Log.Debug($"Finds the element {element}");
            waitForPageToLoad();
            int timeoutForFindElement = timeout.Length < 1 ? DEFAULT_TIMEOUT : timeout[0];
            waitForPageToLoad();
            try
            {
                (new WebDriverWait(driver(), TimeSpan.FromSeconds(timeoutForFindElement)))
                        .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
                return driver().FindElement(element);
            }
            catch (Exception)
            {
                throw new Exception("Failure finding element");
            }
        }


        public static void clickOnElement(By element, params int[] timeout)
        {
            Log.Debug($"Click the specified element {element}");
            int timeoutForFindElement = timeout.Length < 1 ? DEFAULT_TIMEOUT : timeout[0];
            waitForPageToLoad();
            try
            {
                (new WebDriverWait(driver(), TimeSpan.FromSeconds(timeoutForFindElement)))
                    .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
                driver().FindElement(element).Click();
            }
            catch (Exception)
            {
                throw new Exception("Failure clicking on element");
            }
            waitForPageToLoad();
        }

 
        public void switchToFrame(By xpath)
        {
            Log.Debug($"Switchs to frame {xpath}");
            driver().SwitchTo().Frame(findElement(xpath));
        }


        public void switchToDefaultContent()
        {
            Log.Debug("Switchs the content of the to default");
            driver().SwitchTo().DefaultContent();
        }


        public void scrollTo(int xPosition = 0, int yPosition = 0)
        {
            Log.Debug($"Scrolls to XY position ({xPosition},{yPosition})");
            var js = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
            ((IJavaScriptExecutor)driver()).ExecuteScript(js);
        }


        public IWebElement scrollToView(By selector)
        {
            Log.Debug($"Scrolls to view element by selector {selector}");
            var element = driver().FindElement(selector);
            scrollToView(element);
            return element;
        }


        public void scrollToView(IWebElement element)
        {
            Log.Debug($"Scrolls to view element {element}");
            if (element.Location.Y > 200)
            {
                scrollTo(0, element.Location.Y - 100); // Make sure element is in the view but below the top navigation pane
            }

        }

        



    }
}
