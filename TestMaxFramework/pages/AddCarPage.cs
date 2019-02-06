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
using Serilog;


namespace TestMaxFramework.pages
{
    class AddCarPage : BasePage
    {
        private static AddCarPage instance;
        public static AddCarPage Instance = (instance != null) ? instance : new AddCarPage();

        public AddCarPage()
        {
            pageURL = "/supplier/cars/add";
            pageTitle = "Add Car";
        }

        
        By passangers = By.Name("passangers");
        By doors = By.Name("doors");
        By transmission = By.Name("transmission");
        By price = By.Name("locations[1][price]");
        By location = By.XPath("//*[@id='s2id_pickuplocationlist1']/a/span[1]");
        By drop = By.XPath("//*[@id='s2id_dropofflocationlist1']/a/span[1]");
        By carname = By.Name("carname");
        By frameD = By.XPath("//*[@id='cke_1_contents']/iframe");
        By description = By.ClassName("cke_editable");
        By add = By.Id("add");



        public void CreateCar()
        {
            Log.Information("Start creating car profile...");
            Car car = new Car().FillIn();
            Log.Information("Random data generated");
            selectByIndex(passangers, car.Passangers, 30);
            selectByIndex(doors, car.Doors, 30);
            selectByString(transmission, car.Transmission,30);
            findElement(carname).SendKeys(car.Carname);
            switchToFrame(frameD);
            findElement(description).SendKeys("Standard description for car");
            switchToDefaultContent();
            selectByValue(location, car.Location, 60); ;
            selectByValue(drop, car.Location, 60);
            sleepFor(300);
            findElement(price, 30).SendKeys(car.Price.ToString());
            findElement(add).Click();
            Log.Information($"Car {car.Carname} profile created");
        }

    }
}
