using Application.Controllers;
using AutoMapper;
using Infrastructure.Interfaces.Controllers;
using Domain.Context.TrainGovernorContext;
using Domain.Entity.TrainGovernor;
using Domain.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controllers
{
    public class CityTests
    {
        private readonly Mock<ITrainGovernorContext> _context = new Mock<ITrainGovernorContext>();
        private readonly TrainGovernorContext _ctx = new TrainGovernorContext();
        private readonly Mock<ILogger<CityController>> _logger = new Mock<ILogger<CityController>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        CityController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CityController(_ctx, _logger.Object, _mapper.Object);
        }

        [Test]
        public void Get_Cities_Should_Retrieve_3_Records()
        {
            var a = _controller.GetCities();
        }
    }
}
