using Domain.Interfaces.Controllers;
using Infrastructure.Interfaces.Context;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controllers
{
    public class City
    {
        Mock<ICityController> _controller;
        Mock<ITrainGovernorContext> _context;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<ITrainGovernorContext>();
            _controller = new Mock<ICityController>();
        }

        [Test]
        public void Get_Cities_Should_Retrieve_3_Records()
        {

        }
    }
}
