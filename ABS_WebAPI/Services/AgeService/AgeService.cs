using ABS_WebAPI.Models;

namespace ABS_WebAPI.Services.AgeService
{
    public class AgeService : IAgeService
    {

        

        public ResultAgeClass GetAllAges(int regionId, int sexId)
        {
            DatabaseContext context = new DatabaseContext();
            return context.GetAllAgesDb(regionId, sexId);
   
        }


        public ResultAgeYearClass GetAgeStructDiff(int regionId, int sexId, string year1, string year2)
        {
            DatabaseContext context = new DatabaseContext();
            return context.GetAgeStructDiffYears(regionId, sexId,year1,year2);

        }

    }
}
