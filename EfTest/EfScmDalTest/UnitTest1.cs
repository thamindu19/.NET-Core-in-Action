using System;
using System.Linq;
using EfScmDataAccess;
using Xunit;

namespace EfScmDalTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Context maintains the database connection
            using (var ctxt = new EfScmContext())
            {
                // Makes a unique part name so the unit test can be run multiple times
                var partName = "Sample" + DateTime.Now.ToString("HHmmss");
                var part = new PartType()
                {
                    Name = partName
                };
                // Adds the part to the DbSet
                ctxt.Parts.Add(part);
                // Commits the new PartType to the database
                ctxt.SaveChanges();

                // EF translates LINQ queries to SQL queries for you
                var getPart = ctxt.Parts.Single(
                    p => p.Name == partName);
                Assert.Equal(getPart.Name, part.Name);

                // Deleting data is as simple as creating data
                ctxt.Parts.Remove(getPart);
                ctxt.SaveChanges();

                // Checks that the part is no longer there
                getPart = ctxt.Parts.FirstOrDefault(
                    p => p.Name == partName);
                Assert.Null(getPart);
            }
        }
    }
}
