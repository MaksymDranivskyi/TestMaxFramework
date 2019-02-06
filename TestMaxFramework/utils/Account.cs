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
    class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


        public Account FillIn()
        {
            new Faker<Account>()
                .StrictMode(true)
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Phone, f => f.Person.Phone)
                .RuleFor(bp => bp.Password, f => f.Internet.Password())
                .RuleFor(bp => bp.Email, f => f.Internet.Email())
                .Populate(this);

            return this;
        }
    }
}
