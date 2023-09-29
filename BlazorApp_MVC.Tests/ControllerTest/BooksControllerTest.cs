using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorApp_MVC.Controllers;
using BlazorApp_MVC.Interfaces;
using BlazorApp_MVC.Models;
using BlazorApp_MVC.Utilities;
using Castle.Core.Logging;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlazorApp_MVC.Tests.ControllerTest
{
    public class BooksControllerTest
    {
        //SUT - the books controller
        private BooksController _booksController;

        //the three params to fake
        private readonly IDbDapper _dbDapper;
        private readonly Utility _utility;
        private readonly ILogger<BooksController> _logger;

        // Microsoft class
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public BooksControllerTest() {

            //fake dependencies with FakeItEasy for mocking
            _dbDapper = A.Fake<IDbDapper>();
            _utility = A.Fake<Utility>();
            _logger = A.Fake<Microsoft.Extensions.Logging.ILogger<BooksController>>();
            //_httpContextAccessor = A.Fake<HttpContextAccessor>();

            //SUT
            _booksController = new BooksController(_logger, _dbDapper, _utility);
        }

        [Fact]
        public void BooksController_Index_ReturnsListOfAthing()
        {
            #region Arrange - what do i need to bring in
            // mock the data 
            var books = A.Fake<IEnumerable<athing>>();

            //mock the call to the db
            A.CallTo(() => _dbDapper.GetAthingsAsync()).Returns(books);

            #endregion


            #region Act
            Task<IActionResult> result = _booksController.Index();

            #endregion


            #region Assert
            //check types
            result.Should().BeOfType<Task<IActionResult>>();

            #endregion
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(1001)]
        public void BookController_Details_ReturnsAthing(int id)
        {
            #region Arrange
            //mock the data
            var athing = A.Fake<athing>();

            //mock the call to the db
            A.CallTo(() => _dbDapper.GetAthingAsync(id)).Returns(athing);
            #endregion

            #region Act
            var result = _booksController.Details(id);

            #endregion

            #region Assert
            result.Should().BeOfType<Task<IActionResult>>();

            #endregion
        }
    }
}
