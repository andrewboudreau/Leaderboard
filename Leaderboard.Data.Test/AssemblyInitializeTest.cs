using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// http://msdn.microsoft.com/en-us/library/microsoft.visualstudio.testtools.unittesting.assemblyinitializeattribute.aspx
namespace Leaderboard.Data.Test
{
    [TestClass]
    public class AssemblyInitializeTest
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<LeaderboardContext>());
        }

        [TestMethod]
        public void ConclusiveTest()
        {
            Assert.IsTrue(true);
        }
    }
}
