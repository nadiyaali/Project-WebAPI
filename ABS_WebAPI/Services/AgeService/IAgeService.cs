using ABS_WebAPI.Models;

namespace ABS_WebAPI.Services.AgeService
{

    // Implemented the Repository Pattern by creating a service
    public interface IAgeService
    {

        ResultAgeClass GetAllAges(int regionCode, int sexId);

        ResultAgeYearClass GetAgeStructDiff(int regionCode, int sexId, string year1, string year2 );

    }
}
