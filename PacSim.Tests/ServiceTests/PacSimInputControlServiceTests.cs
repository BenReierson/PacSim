using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Moq;
using NUnit.Framework;
using PacSim.Services;

namespace PacSim.ServiceTests
{
    public class PacSimInputControlServiceTests
    {
        Mock<IPacSimMovementService> mockMovementService;

        [SetUp]
        public void SetupMoqs()
        {
            mockMovementService = new Mock<IPacSimMovementService>();
        }

        [Test]
        public void VerifyPlaceCalledOnInput()
        {
            var service = new PacSimInputControlService(mockMovementService.Object);

            //execute valid place command
            var command = service.ParseCommand("PLACE 0,0,NORTH");

            Assert.NotNull(command);

            command.Invoke();

            //verify place was called in movement service
            mockMovementService.Verify(s => s.Place(0, 0, Models.PacDirection.NORTH));

            //verify nothing else was called
            mockMovementService.VerifyNoOtherCalls();
        }

        [Test]
        public void VerifyMoveCalledOnInput()
        {
            var service = new PacSimInputControlService(mockMovementService.Object);

            //execute valid move command
            var command = service.ParseCommand("MOVE");

            Assert.NotNull(command);

            command.Invoke();

            //verify move was called in movement service
            mockMovementService.Verify(s => s.Move());

            //verify nothing else was called
            mockMovementService.VerifyNoOtherCalls();
        }

        [Test]
        public void VerifyLeftCalledOnInput()
        {
            var service = new PacSimInputControlService(mockMovementService.Object);

            //execute valid left command
            var command = service.ParseCommand("LEFT");

            Assert.NotNull(command);

            command.Invoke();

            //verify left was called in movement service
            mockMovementService.Verify(s => s.Left());

            //verify nothing else was called
            mockMovementService.VerifyNoOtherCalls();
        }

        [Test]
        public void VerifyRightCalledOnInput()
        {
            var service = new PacSimInputControlService(mockMovementService.Object);

            //execute valid right command
            var command = service.ParseCommand("RIGHT");

            Assert.NotNull(command);

            command.Invoke();

            //verify right was called in movement service
            mockMovementService.Verify(s => s.Right());

            //verify nothing else was called
            mockMovementService.VerifyNoOtherCalls();
        }

        [Test]
        public void VerifyReportCalledOnInput()
        {
            var service = new PacSimInputControlService(mockMovementService.Object);

            //execute valid report command
            var command = service.ParseCommand("REPORT");

            Assert.NotNull(command);

            command.Invoke();

            //verify report was called in movement service
            mockMovementService.Verify(s => s.Report());

            //verify nothing else was called
            mockMovementService.VerifyNoOtherCalls();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("PLACE X,Y,F", "PLACE 0,0,0,NORTH", "PLACE 0,0,NORTH,", "MOVE ", "MOVEMOVE", "MOVE MOVE", "LEFTT", "RIGH", "LEFT RIGHT", "LEFT/nRIGHT")]
        [TestCase("LOTS OF NAUGHTY STRINGS",
            @"!@#$%^&*()_+",
            @"1;DROP TABLE users",
            "true", "FALSE",
            @"-9223372036854775808/-1",
            "NaN", "Null",
            @"­؀؁؂؃؄؅؜۝܏᠎​‌‍‎‏‪‫‬‭‮⁠⁡⁢⁣⁤⁦⁧⁨⁩⁪⁫⁬⁭⁮⁯﻿￹￺￻𑂽𛲠𛲡𛲢𛲣𝅳𝅴𝅵𝅶𝅷𝅸𝅹𝅺󠀁󠀠󠀡󠀢󠀣󠀤󠀥󠀦󠀧󠀨󠀩󠀪󠀫󠀬󠀭󠀮󠀯󠀰󠀱󠀲󠀳󠀴󠀵󠀶󠀷󠀸󠀹󠀺󠀻󠀼󠀽󠀾󠀿󠁀󠁁󠁂󠁃󠁄󠁅󠁆󠁇󠁈󠁉󠁊󠁋󠁌󠁍󠁎󠁏󠁐󠁑󠁒󠁓󠁔󠁕󠁖󠁗󠁘󠁙󠁚󠁛󠁜󠁝󠁞󠁟󠁠󠁡󠁢󠁣󠁤󠁥󠁦󠁧󠁨󠁩󠁪󠁫󠁬󠁭󠁮󠁯󠁰󠁱󠁲󠁳󠁴󠁵󠁶󠁷󠁸󠁹󠁺󠁻󠁼󠁽󠁾󠁿",
            @"ЁЂЃЄЅІЇЈЉЊЋЌЍЎЏАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдежзийклмнопрстуфхцчшщъыьэюя",
            @"<foo val=“bar” />",
            @"田中さんにあげて下さい",
            @"Ⱥ",
            @",。・:*:・゜’( ☻ ω ☻ )。・:*:・゜’",
            @"❤️ 💔 💌 💕 💞 💓 💗 💖 💘 💝 💟 💜 💛 💚 💙",
            @"﷽",
            @"test⁠test‫",
            @"Ṱ̺̺̕o͞ ̷i̲̬͇̪͙n̝̗͕v̟̜̘̦͟o̶̙̰̠kè͚̮̺̪̹̱̤ ̖t̝͕̳̣̻̪͞h̼͓̲̦̳̘̲e͇̣̰̦̬͎ ̢̼̻̱̘h͚͎͙̜̣̲ͅi̦̲̣̰̤v̻͍e̺̭̳̪̰-m̢iͅn̖̺̞̲̯̰d̵̼̟͙̩̼̘̳ ̞̥̱̳̭r̛̗̘e͙p͠r̼̞̻̭̗e̺̠̣͟s̘͇̳͍̝͉e͉̥̯̞̲͚̬͜ǹ̬͎͎̟̖͇̤t͍̬̤͓̼̭͘ͅi̪̱n͠g̴͉ ͏͉ͅc̬̟h͡a̫̻̯͘o̫̟̖͍̙̝͉s̗̦̲.̨̹͈̣",
            @"⒯⒣⒠ ⒬⒰⒤⒞⒦ ⒝⒭⒪⒲⒩ ⒡⒪⒳ ⒥⒰⒨⒫⒮ ⒪⒱⒠⒭ ⒯⒣⒠ ⒧⒜⒵⒴ ⒟⒪⒢",
            @"<script>alert(123)</script>",
            @"{0}",
            @"🏳0🌈️")]
        public void DetectInvalidInputs(params string[] inputs)
        {
            var service = new PacSimInputControlService(mockMovementService.Object);

            foreach (var input in inputs)
            {
                Assert.Throws<ArgumentException>(() => service.ParseCommand(input));
            }

            mockMovementService.VerifyNoOtherCalls();
        }

        [TestCase("PLACE 0,0,NORTH", ExpectedResult ="0,0,NORTH")]
        [TestCase("PLACE 0,0,NORTH", "MOVE", "REPORT", ExpectedResult = "0,1,NORTH")]
        [TestCase("PLACE 0,0,NORTH", "LEFT", "REPORT", ExpectedResult = "0,0,WEST")]
        [TestCase("PLACE 1,2,EAST", "MOVE", "MOVE", "LEFT", "MOVE", "REPORT", ExpectedResult = "3,3,NORTH")]
        public string ExecuteAllInputsOnRealFiveByFiveGridService(params string[] inputs)
        {
            var service = new PacSimInputControlService(new PacSimGridMovementService(5,5));

            string finalResult = null;

            foreach (var input in inputs)
            {
                try
                {
                    finalResult = service.ParseCommand(input)?.Invoke();
                }
                catch (Exception) { }//ignore invalid inputs
            }

            return finalResult;
        }
    }
}
