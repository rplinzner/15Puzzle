using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solvers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Node node = new Node(4, 4, new byte[] {1, 2, 3, 0, 4, 6, 7, 5, 8}, MoveEnum.N, null, 0);

            BfsSolver solver = new BfsSolver("RDUL", new NodeDTO()
            {
                Board = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 0, 15 },
                X = 4,
                Y = 4
            }, new WritePathDTO()
            {

            });
            solver.Solve();




        }
    }
}
