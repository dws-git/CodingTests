using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using MarsRover;

namespace MarsRover.Tests
{
    public class UnitTests
    {
        private readonly ITestOutputHelper output;
        private readonly string marsRoverImput =
            String.Format("55{0}12N{0}LMLMLMLMM{0}33E{0}MMRMMRMRRM{0}", Environment.NewLine);
        private readonly string marsRoverOutput =
            String.Format("13N{0}51E{0}", Environment.NewLine);


        public UnitTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void MarsRover1Test()
        {
            string[] input = marsRoverImput.Split(new string[]{Environment.NewLine}, StringSplitOptions.None);
            string[] marsOutput = marsRoverOutput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var rover1 = new Rover(input[0], input[1]);
            rover1.ExecuteCommands(input[2]);
            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                rover1.PrintLocation();
                string actual = writer.ToString();
                Assert.Equal($"{marsOutput[0]}{Environment.NewLine}", actual);
                output.WriteLine(actual);
            }
        }

        [Fact]
        public void MarsRover2Test()
        {
            string[] input = marsRoverImput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string[] marsOutput = marsRoverOutput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var rover2 = new Rover(input[0], input[3]);
            rover2.ExecuteCommands(input[4]);
            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                rover2.PrintLocation();
                string actual = writer.ToString();
                Assert.Equal($"{marsOutput[1]}{Environment.NewLine}", actual);
                output.WriteLine(actual);
            }
        }

    }
}
