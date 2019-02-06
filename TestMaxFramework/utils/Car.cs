using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestMaxFramework.pages;
using TestMaxFramework.utils;

using Bogus;

namespace TestMaxFramework
{
    class Car
    {

        public int  Passangers { get; set; }
        public int Doors { get; set; }
        public string Transmission { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }
        public int Taxvalue { get; set; }
        public string Carname { get; set; }


        public Car FillIn()
        {
            new Faker<Car>()
                .StrictMode(true)
                .RuleFor(o => o.Carname, f => f.Company.CompanyName())
                .RuleFor(o => o.Passangers, f => f.Random.Number(0, 2))
                .RuleFor(o => o.Doors, f => f.Random.Number(0, 3))
                .RuleFor(bp => bp.Transmission, f => f.PickRandom("Auto","Manual","Other"))
                .RuleFor(bp => bp.Location, f => f.PickRandom("London", "Sambu", "Berlin", "New York"))
                .RuleFor(o => o.Price, f => f.Random.Number(1000, 5000))
                .RuleFor(o => o.Taxvalue, f => f.Random.Number(1000, 5000))
                .Populate(this);

            return this;
        }
    }
}
