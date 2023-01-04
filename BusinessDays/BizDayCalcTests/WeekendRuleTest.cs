using System;
using System.Collections.Generic;
using BizDayCalc;
using Xunit;

namespace BizDayCalcTests {
    public class WeekendRuleTest {
        [Fact]
        public void TestCheckIsBusinessDay() {
            var rule = new WeekendRule();
            Assert.True(rule.CheckIsBusinessDay(new DateTime(2016, 6, 27)));
            Assert.False(rule.CheckIsBusinessDay(new DateTime(2016, 6, 26)));
        }

        // [Theory]
        // [InlineData("2016-06-27")] // Monday
        // [InlineData("2016-03-01")] // Tuesday
        // [InlineData("2017-09-20")] // Wednesday
        // [InlineData("2017-09-17")] // Sunday
        // public void IsBusiniessDay(String date) {
        //     var rule = new WeekendRule();
        //     Assert.True(rule.CheckIsBusinessDay(DateTime.Parse(date)));
        // }

        [Theory]
        [InlineData("2016-06-26")] // Sunday
        [InlineData("2016-11-12")] // Saturday
        public void IsNotBusinessDay(String date) {
            var rule = new WeekendRule();
            Assert.False(rule.CheckIsBusinessDay(DateTime.Parse(date)));
        }

        [Theory]
        [InlineData(true, "2016-06-27")]
        [InlineData(true, "2016-03-01")]
        [InlineData(false, "2016-06-26")]
        [InlineData(false, "2016-11-12")]
        public void IsBusinessDay(bool expected, string date) {
            var rule = new WeekendRule();
            Assert.Equal(expected, rule.CheckIsBusinessDay(DateTime.Parse(date)));
        }
        
        public static IEnumerable<object[]> Days {
            get {
                yield return new object[] {true, new DateTime(2016, 6, 27)};
                yield return new object[] {true, new DateTime(2016, 3, 1)};
                yield return new object[] {false, new DateTime(2016, 6, 26)};
                yield return new object[] {false, new DateTime(2016, 11, 12)};
            }
        }

        // [Theory]
        // [MemberData(nameOf(Days))]
        // public void TestCheckIsBusinessDay(bool expected, DateTime date) {
        //     var rule = new WeekendRule();
        //     Assert.Equal(expected, rule.CheckIsBusinessDay(date));
        // }
    }
}
