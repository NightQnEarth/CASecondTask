using System;
using System.IO;
using NUnit.Framework;

namespace CASecondTask.Tests
{
    [TestFixture]
    public class Tests
    {
        private readonly DepthFirstSearch _finder = new DepthFirstSearch();
        
        private bool CheckCorrect(string inputLines, string expectedResult)
        {
            var tempFileName = Path.GetTempFileName();
            
            using (StreamWriter writer = new StreamWriter(tempFileName)) 
                writer.Write(inputLines);

            var actualResult = GetActualResult();

            try
            {
                File.Delete(tempFileName);
            }
            catch (Exception) { }
            
            return expectedResult.Equals(actualResult);

            string GetActualResult()
            {
                using (StreamReader reader = new StreamReader(tempFileName))
                {
                    var graph = Program.GetInputData(reader.ReadLine);
                    var resultChain = _finder.GetCycle(graph);
                    return Program.ResultGenerate(resultChain);
                }
            }
        }
        
        [Test]
        public void DescriptionFromTaskTest()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "2 3 0",
                "1 3 0",
                "1 2 4 0",
                "3 0");
            
            var expectedResult = string.Join(
                Environment.NewLine,
                "N",
                "1",
                "2",
                "3");
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void TwoNodesAcyclicGraphTest()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "2",
                "2 0",
                "1 0");

            const string expectedResult = "A";
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void ThreeNodesAcyclicGraphTest()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "3",
                "2 3 0",
                "1 0",
                "1 0");

            const string expectedResult = "A";
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void ThreeNodesCyclicGraphTest()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "3",
                "2 3 0",
                "1 3 0",
                "1 2 0");

            var expectedResult = string.Join(
                Environment.NewLine,
                "N",
                "1",
                "2",
                "3");
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void FourNodesAcyclicGraphTest()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "2 3 4 0",
                "1 0",
                "1 0",
                "1 0");

            const string expectedResult = "A";
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void FourNodesCyclicGraphTest()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "2 3 4 0",
                "1 4 0",
                "1 0",
                "1 2 0");

            var expectedResult = string.Join(
                Environment.NewLine,
                "N",
                "1",
                "2",
                "4");
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void DisconnectedGraphWithOneCyclesTest()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "11",
                "2 3 4 0",
                "1 0",
                "1 0",
                "1 0",
                "6 7 0",
                "5 0",
                "5 0",
                "9 10 11 0",
                "8 11 0",
                "8 0",
                "8 9 0");
            
            var expectedResult = string.Join(
                Environment.NewLine,
                "N",
                "8",
                "9",
                "11");
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void DisconnectedGraphWithoutCyclesTest()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "9",
                "2 0",
                "1 0",
                "4 5 0",
                "3 0",
                "3 0",
                "7 8 9 0",
                "6 0",
                "6 0",
                "6 0");
            
            const string expectedResult = "A";
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void FirstNodeNotInCycleTest()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "2 0",
                "1 3 4 0",
                "2 4 0",
                "2 3 0");

            var expectedResult = string.Join(
                Environment.NewLine,
                "N",
                "2",
                "3",
                "4");
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void IgnoreLoopsAndMultipleEdgesTest()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "1 2 2 0",
                "1 3 4 0",
                "2 4 3 0",
                "2 3 3 0");

            var expectedResult = string.Join(
                Environment.NewLine,
                "N",
                "2",
                "3",
                "4");
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
    }
}