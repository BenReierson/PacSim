using NUnit.Framework;
using PacSim.Services;
using PacSim.Models;
using System;

namespace PacSim.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var service = new PacSimGridMovementService(5, 5);

            Assert.IsNull(service.Report());

            Assert.Throws<InvalidOperationException>(service.Move);

            service.Place(0, 0, PacDirection.NORTH);

            service.Move();
            service.Move();
        }
    }
}