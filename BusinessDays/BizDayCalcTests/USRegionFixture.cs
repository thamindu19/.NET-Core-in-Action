using BizDayCalc;
using System;
using Xunit;

namespace BizDayCalcTests {
    public class USRegionFixture {
        // Sharing context with class fixtures
        public Calculator Calc {get; private set;}
        
        public USRegionFixture() {
            Calc = new Calculator();
            Calc.AddRule(new WeekendRule());
            Calc.AddRule(new HolidayRule());
            Console.WriteLine("Hello");
        }

        // Sharing context with collection fixtures
        [CollectionDefinition("US region collection")]
        public class USRegionCollection : ICollectionFixture<USRegionFixture> {

        }
    }
}