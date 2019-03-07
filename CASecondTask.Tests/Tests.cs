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
    }
}