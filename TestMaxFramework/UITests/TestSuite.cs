using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using TestMaxFramework.utils;
using TestMaxFramework.pages;
using OpenQA.Selenium.Support.PageObjects;
using Serilog;
using Serilog.Events;

namespace TestMaxFramework
{
    [TestFixture]
    class TestSuite : BaseTest
    {

        [Test]
        [Order(1)]
        [Author("Maksym Dramivskyi")]
        [Category("UI test")]
        public void ProfileUpdateTest()
        {
            Log.Information("Profile update test starting...");
            AccountPage account = AccountPage.Instance;    
            account.open();
            account.updateUser();
            Log.Information("Profile update test finished");
        }

        [Test]
        [Order(2)]
        [Author("Maksym Dramivskyi")]
        [Category("UI test")]
        public void AddCarTest()
        {
            Log.Information("Add car test starting...");
            CarsPage cars = CarsPage.Instance;
            cars.open();
            cars.AddCar();
            AddCarPage addCar = AddCarPage.Instance;
            addCar.CreateCar();
            Log.Information("Add car test finished");
        }

        [Test]
        [Order(3)]
        [Author("Maksym Dramivskyi")]
        [Category("UI test")]
        public void DeleteCarTest()
        {
            Log.Information("Delete car test starting...");
            CarsPage cars = CarsPage.Instance;
            cars.open();
            cars.AddCar();
            AddCarPage addCar = AddCarPage.Instance;
            addCar.CreateCar();
            cars.DeleteCar();
            Log.Information("Delete car test finished");

        }
        
    }
}
