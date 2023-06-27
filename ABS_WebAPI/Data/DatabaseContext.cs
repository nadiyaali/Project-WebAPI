using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABS_WebAPI.Models;
using Newtonsoft.Json;

/* Add to appsetting.json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLExpress;Database=ABS_SA4_DATA;Trusted_Connection=true;TrustServerCertificate=true;"
  },
*/

namespace ABS_WebAPI.Data
{
    public class DatabaseContext
    {

        private string _connString = "Server=DESKTOP-V2RFM0L;Database=ABS_DATA;Trusted_Connection=yes;";

        public DatabaseContext() { }

        //  /api/age-structure/{SA4 Code}/{Sex}
        //  /api/age-structure/102/1
        public ResultAgeClass GetAllAgesDb(int regionId, int sexId)
        {
            string quRegion = $"SELECT REGION, REGION_TEXT from DIM_REGION WHERE REGION={regionId}";
            ResultAgeClass result = new ResultAgeClass();

            using (SqlConnection sqlCon = new SqlConnection(_connString))
            {
                sqlCon.Open();
                // Run region query
                SqlCommand cmnd = new SqlCommand(quRegion, sqlCon);
                SqlDataReader rdr = cmnd.ExecuteReader();
                while (rdr.Read())
                {
                    result.regionCode = (int)rdr["REGION"];
                    result.regionName = rdr["REGION_TEXT"].ToString();

                }
                rdr.Close();

                //Run population query
                string quPopulation = $"Select AGE as age, SEX_TEXT as sex, VALUE_POPULATION as population from FACT_POPULATION WHERE REGION={regionId} and SEX={sexId}";
                SqlCommand command = new SqlCommand(quPopulation, sqlCon);
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                // Convert DataTAble to List of PopCategory class
                List<PopCategory> popDetails = new List<PopCategory>();
                popDetails = ConverterClass.ConvertDataTable<PopCategory>(dt);

                result.pop = popDetails;
            }
            return result;

        }


        // /api/age-structure-diff/{SA4 Code}/{Sex}/{ Year1}/{ Year2}
        // /api/age-structure-diff/102/1/2011/2016 should return the difference in age between 2016 and 2011 Census Years
        public ResultAgeYearClass GetAgeStructDiffYears(int regionId, int sexId, string year1, string year2)
        {
            string quRegion = $"SELECT REGION, REGION_TEXT from DIM_REGION WHERE REGION={regionId}";

            ResultAgeYearClass result = new ResultAgeYearClass();

            using (SqlConnection sqlCon = new SqlConnection(_connString))
            {
                sqlCon.Open();
                // Run region query
                SqlCommand cmnd = new SqlCommand(quRegion, sqlCon);
                SqlDataReader rdr = cmnd.ExecuteReader();
                while (rdr.Read())
                {
                    result.regionCode = (int)rdr["REGION"];
                    result.regionName = rdr["REGION_TEXT"].ToString();

                }
                rdr.Close();

                //Run population query

                string quPopulation = $"Select Age as age, SEX_TEXT as sex, VALUE_POPULATION as pop, CENSUS_YEAR as year from FACT_POPULATION WHERE REGION =  {regionId} and SEX = {sexId} and CENSUS_YEAR in ({year1},{year2})";
                SqlCommand command = new SqlCommand(quPopulation, sqlCon);
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                // Convert DataTAble to List of PopCategory class
                List<PopCategoryYear> popDetails = new List<PopCategoryYear>();
                popDetails = ConverterClass.ConvertDataTable<PopCategoryYear>(dt);

                result.pop = popDetails;
                //sqlCon.Close();

            }
            return result;

        }
    }
}



