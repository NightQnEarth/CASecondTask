using System;
using System.IO;
using NUnit.Framework;

namespace CASecondTask.Tests
{
    [TestFixture]
    public class Tests
    {
        private const string ExpectedAcyclicResult = "A";

        private static string GetActualResult(string inputLines)
        {
            var tempFileName = Path.GetTempFileName();

            using (var writer = new StreamWriter(tempFileName))
                writer.Write(inputLines);

            var actualResult = GetActualResult();

            try
            {
                File.Delete(tempFileName);
            }
            catch (IOException) { }

            return actualResult;

            string GetActualResult()
            {
                using (var reader = new StreamReader(tempFileName))
                {
                    var graph = DataParser.GetInputData(reader.ReadLine);
                    var resultCycle = DepthFirstSearch.GetCycle(graph);
                    return DataParser.ResultGenerate(resultCycle);
                }
            }
        }

        [Test]
        public void SampleInputFromReadme_ReturnCycle()
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

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void TwoNodesGraph_ReturnA()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "2",
                "2 0",
                "1 0");

            Assert.AreEqual(ExpectedAcyclicResult, GetActualResult(inputLines));
        }

        [Test]
        public void ThreeNodesAcyclicGraph_ReturnA()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "3",
                "2 3 0",
                "1 0",
                "1 0");

            Assert.AreEqual(ExpectedAcyclicResult, GetActualResult(inputLines));
        }

        [Test]
        public void ThreeNodesCyclicGraph_ReturnCycle()
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

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void FourNodesAcyclicGraph_ReturnA()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "2 3 4 0",
                "1 0",
                "1 0",
                "1 0");

            Assert.AreEqual(ExpectedAcyclicResult, GetActualResult(inputLines));
        }

        [Test]
        public void FourNodesCyclicGraph_ReturnCycle()
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

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void DisconnectedGraphWithOneCycle_ReturnCycle()
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

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void DisconnectedGraphWithoutCycles_ReturnA()
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

            Assert.AreEqual(ExpectedAcyclicResult, GetActualResult(inputLines));
        }

        [Test]
        public void CyclicGraphWithFirstNodeNotInCycle_ReturnCycle()
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

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void GraphWithLoopsAndMultipleEdges_ReturnCycle()
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

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void SixNodesRingGraphWithOneBigCycle_ReturnCycle()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "6",
                "2 6 0",
                "1 3 0",
                "2 4 0",
                "3 5 0",
                "4 6 0",
                "5 1 0");

            var expectedResult = string.Join(
                Environment.NewLine,
                "N",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void SixNodesRingGraphWithTwoCycles_ReturnCycle()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "6",
                "2 6 0",
                "1 3 0",
                "2 4 5 0",
                "3 5 0",
                "3 4 6 0",
                "1 5 0");

            var expectedResult = string.Join(
                Environment.NewLine,
                "N",
                "3",
                "4",
                "5");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void SixNodesRingGraphWithThreeCycles_ReturnCycle()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "6",
                "2 4 6 0",
                "1 3 0",
                "2 4 5 0",
                "1 3 5 0",
                "3 4 6 0",
                "1 5 0");

            var expectedResult = string.Join(
                Environment.NewLine,
                "N",
                "1",
                "4",
                "5",
                "6");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void SixDisconnectedNodesGraph_ReturnA()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "6",
                "0",
                "0",
                "0",
                "0",
                "0",
                "0");

            Assert.AreEqual(ExpectedAcyclicResult, GetActualResult(inputLines));
        }

        [Test]
        public void SixNodesAlmostRingGraph_ReturnA()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "6",
                "2 0",
                "1 3 0",
                "2 4 0",
                "3 5 0",
                "4 6 0",
                "5 0");

            Assert.AreEqual(ExpectedAcyclicResult, GetActualResult(inputLines));
        }
    }
}