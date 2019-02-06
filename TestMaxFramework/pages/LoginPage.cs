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
using TestMaxFramework.utils;

namespace TestMaxFramework.pages
{
    class LoginPage:BasePage
    {
        private static LoginPage instance;
        public static LoginPage Instance = (instance != null) ? instance : new LoginPage();

        public LoginPage()
        {
            pageURL = "/supplier";
            pageTitle = "Supplier Login";
        }
        
        By email = By.XPath("//div/form[1]/div[1]/input[1]");
        By password = By.Name("password");
        By loginBtn = By.XPath("//div/form[1]/button");

        public void loginUser()
        {
            findElement(email).SendKeys("supplier@phptravels.com");
            findElement(password).SendKeys("demosupplier");
            clickOnElement(loginBtn);
            Log.Information($"User {email} logined");

        }

    }
}
