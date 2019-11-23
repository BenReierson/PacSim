using System;
using System.Security.Cryptography;
using NUnit.Framework;
using PacSim.Models;
using PacSim.Services;

namespace PacSim.ServiceTests
{
    public class PacSimGridMovementServiceTests
    {

        [TestCase(int.MinValue,int.MinValue)]
        [TestCase(0,0)]
        [TestCase(1,0)]
        [TestCase(0,1)]
        [TestCase(5,5)]
        [TestCase(1, int.MaxValue)]
        [TestCase(int.MaxValue,1)]
        [TestCase(int.MaxValue,int.MaxValue)]
        public void NewGridService(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                Assert.Throws<ArgumentException>(()=>new PacSimGridMovementService(width, height));
            }
            else
            {
                var service = new PacSimGridMovementService(width, height);

                Assert.AreEqual(width, service.GridWidth);
                Assert.AreEqual(height, service.GridHeight);

                Assert.IsNull(service.Report(), "Non-placed Pac position should be null");

                Assert.Throws<InvalidOperationException>(service.Move, "Moving non-placed Pac should throw exception.");
                Assert.Throws<InvalidOperationException>(service.Left, "Turning non-placed Pac should throw exception.");
                Assert.Throws<InvalidOperationException>(service.Right, "Turning non-placed Pac should throw exception.");
            }
        }

        [Test]
        public void PlacePacOnFiveByFiveGrid_Valid([Range(0, 4)]int x, [Range(0, 4)]int y,
                                   [Values(PacDirection.NORTH, PacDirection.SOUTH, PacDirection.EAST, PacDirection.WEST)] PacDirection facing)
        {
            var service = new PacSimGridMovementService(5, 5);

            var placedPosition = service.Place(x, y, facing);

            Assert.AreEqual(x, placedPosition.X);
            Assert.AreEqual(y, placedPosition.Y);
            Assert.AreEqual(facing, placedPosition.Facing);
        }

        [Test]
        public void PlacePacOnFiveByFiveGrid_Invalid([Values(int.MinValue, -1, 5, int.MaxValue)]int x,
                                                     [Values(int.MinValue, -1, 5, int.MaxValue)]int y,
                                                     [Values(PacDirection.NORTH, PacDirection.SOUTH, PacDirection.EAST, PacDirection.WEST)] PacDirection facing)
        {
            var service = new PacSimGridMovementService(5, 5);

            Assert.Throws<ArgumentOutOfRangeException>(()=>service.Place(x, y, facing));

        }

        [TestCase(PacDirection.NORTH, PacDirection.WEST)]
        [TestCase(PacDirection.WEST, PacDirection.SOUTH)]
        [TestCase(PacDirection.SOUTH, PacDirection.EAST)]
        [TestCase(PacDirection.EAST, PacDirection.NORTH)]
        public void TurnPacLeftOnGrid(PacDirection startFacing, PacDirection expectedFacing)
        {
            var service = new PacSimGridMovementService(1, 1);

            service.Place(0, 0, startFacing);

            service.Left();
            var newPos = service.Report();

            Assert.IsNotNull(newPos);
            Assert.AreEqual(expectedFacing, newPos.Value.Facing); 
        }


        [TestCase(PacDirection.NORTH, PacDirection.EAST)]
        [TestCase(PacDirection.WEST, PacDirection.NORTH)]
        [TestCase(PacDirection.SOUTH, PacDirection.WEST)]
        [TestCase(PacDirection.EAST, PacDirection.SOUTH)]
        public void TurnPacRightOnGrid(PacDirection startFacing, PacDirection expectedFacing)
        {
            var service = new PacSimGridMovementService(1, 1);

            service.Place(0, 0, startFacing);

            service.Right();
            var newPos = service.Report();

            Assert.IsNotNull(newPos);
            Assert.AreEqual(expectedFacing, newPos.Value.Facing);
        }

        [TestCase(1,1, 1, 0,0,PacDirection.NORTH, 0,0)]
        [TestCase(1,1, 9, 0,0,PacDirection.NORTH, 0,0)]
        [TestCase(5,5, 1, 0,0,PacDirection.NORTH, 0,1)]
        [TestCase(5,5, 9, 0,0,PacDirection.NORTH, 0,4)]
        [TestCase(5,5, 9, 0,4,PacDirection.NORTH, 0,4)]
        [TestCase(5,5, 9, 4,4,PacDirection.NORTH, 4,4)]
        [TestCase(1,1, 1, 0,0,PacDirection.SOUTH, 0,0)]
        [TestCase(1,1, 9, 0,0,PacDirection.SOUTH, 0,0)]
        [TestCase(5,5, 1, 0,4,PacDirection.SOUTH, 0,3)]
        [TestCase(5,5, 9, 0,0,PacDirection.SOUTH, 0,0)]
        [TestCase(5,5, 9, 0,4,PacDirection.SOUTH, 0,0)]
        [TestCase(5,5, 9, 4,4,PacDirection.SOUTH, 4,0)]
        [TestCase(1,1, 1, 0,0,PacDirection.EAST, 0,0)]
        [TestCase(1,1, 9, 0,0,PacDirection.EAST, 0,0)]
        [TestCase(5,5, 1, 0,0,PacDirection.EAST, 1,0)]
        [TestCase(5,5, 9, 0,0,PacDirection.EAST, 4,0)]
        [TestCase(5,5, 9, 4,0,PacDirection.EAST, 4,0)]
        [TestCase(5,5, 9, 4,4,PacDirection.EAST, 4,4)]
        [TestCase(1,1, 1, 0,0,PacDirection.WEST, 0,0)]
        [TestCase(1,1, 9, 0,0,PacDirection.WEST, 0,0)]
        [TestCase(5,5, 1, 4,0,PacDirection.WEST, 3,0)]
        [TestCase(5,5, 9, 4,0,PacDirection.WEST, 0,0)]
        [TestCase(5,5, 9, 0,0,PacDirection.WEST, 0,0)]
        [TestCase(5,5, 9, 0,4,PacDirection.WEST, 0,4)]
        public void MovePacOnGrid(int width, int height, int moveTimes, int startX, int startY, PacDirection startFacing, int expectedX, int expectedY)
        {
            var service = new PacSimGridMovementService(width, height);

            service.Place(startX, startY, startFacing);

            try
            {
                for (int i = 0; i < moveTimes; i++) service.Move();
            }
            catch (Exception) { }

            var newPos = service.Report();

            Assert.IsNotNull(newPos);
            Assert.AreEqual(expectedX, newPos.Value.X, "X position doesn't match");
            Assert.AreEqual(expectedY, newPos.Value.Y, "Y position doesn't match");
        }

        [Test]
        public void MovePacInClockWiseCircle()
        {
            var service = new PacSimGridMovementService(3, 3);

            var startPos = service.Place(0, 0, PacDirection.NORTH);

            service.Move();
            service.Move();

            service.Right();

            service.Move();
            service.Move();

            service.Right();

            service.Move();
            service.Move();

            service.Right();

            service.Move();
            service.Move();

            service.Right();

            var endPos = service.Report();

            Assert.AreEqual(startPos, endPos);
        }

        [Test]
        public void MovePacInCounterClockWiseCircle()
        {
            var service = new PacSimGridMovementService(3, 3);

            var startPos = service.Place(2, 2, PacDirection.WEST);

            service.Move();
            service.Move();

            service.Left();

            service.Move();
            service.Move();

            service.Left();

            service.Move();
            service.Move();

            service.Left();

            service.Move();
            service.Move();

            service.Left();

            var endPos = service.Report();

            Assert.AreEqual(startPos, endPos);
        }
    }
}
