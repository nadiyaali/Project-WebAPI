using ABS_WebAPI.Controllers;
using ABS_WebAPI.Models;
using ABS_WebAPI.Services.AgeService;
using Microsoft.AspNetCore.Mvc;

namespace ABS_WebAPI.Test
{
    public class UnitTest1
    {
        [Fact]
        public void GetAgesAll_Returns_Ages_In_Region()
        {
            // There are 234 pop data results for region 106 males
            int count = 234;
            IAgeService ageService = new AgeService();
            var controller = new AgeController(ageService);

            //Act
            var actionResult = controller.GetAllAges(106, 1);
            //Assert
            var result = actionResult.Result as OkObjectResult;
            var returnData = result.Value as ResultAgeClass;
            Assert.Equal(count, returnData.pop.Count());
        }
    }
}