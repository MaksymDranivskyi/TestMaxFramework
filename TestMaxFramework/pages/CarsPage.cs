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
using TestMaxFramework.pages;
using TestMaxFramework.utils;
using FluentAssertions;

namespace TestMaxFramework.pages
{
    class CarsPage : BasePage
    {
        private static CarsPage instance;
        public static CarsPage Instance = (instance != null) ? instance : new CarsPage();
        
        public CarsPage()
        {
            pageURL = "/supplier/cars";
            pageTitle = "Cars Management";
        }

        By add = By.XPath("//div/form/button");
        By deleteBtn = By.ClassName("delete_button");

        public void AddCar()
        {
            clickOnElement(add);
        }

        public void DeleteCar()
        {
            
            findElementIgnoreException(By.CssSelector("tbody > tr.xcrud-row.xcrud-row-0 > td:nth-child(12) > span > .btn-danger"),50).Click();
            sleepFor(500);
            var confirmationAlert = driver().SwitchTo().Alert();
            confirmationAlert.Accept();
        }


    }
}
