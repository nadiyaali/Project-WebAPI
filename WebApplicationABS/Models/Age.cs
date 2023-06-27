namespace WebApplicationABS.Models
{
    /*
    
    SEX_ABS,Sex,AGE,Age,STATE,State,REGIONTYPE,Geography Level, ASGS_2016, Region, TIME, Census year,Value,Flag Codes, Flags
    
    SEX_ABS, 
    Sex,AGE,
    Age,STATE,
    State,
    REGIONTYPE,
    Geography Level, 
    ASGS_2016, 
    Region, 
    TIME, 
    Census year,
    Value,
    Flag Codes, 
    Flags
    */

    /*DATABASE
    



            "STATE",    "State",    "       REGIONTYPE",    "Geography Level",          "ASGS_2016",    "Region"
                1	    New South Wales	    SA4	            Statistical Area Level 4	    102	        Central Coast
    
    Text : DimState
    "STATE",    "State"
    
    Text :DimRegion (Dimension Table)
    "ASGS_2016",    "Region"
    
    Text :DimSex (Dimension Table)
    SEX_ABS,Sex
    
    Text : DimAge (Dimension Table)
    Age, AGE


    Numbers :FactPopulation (Fact Table) 8 cols
    SEX_ABS,    AGE,   STATE,    REGIONTYPE,    ASGS_2016,   TIME,   Census year,    Value,,

    Fact tables contain numerical data 
    Dimension tables provide context and background information.
     
     */


    /* 
    /api/age-structure/{SA4 Code}/{Sex}, 
    /api/age - structure/102/ 1 
    should return all ages for males in Central Coast
    Select * from table where REGIONTYPE = "SA4" and SEX_ABS = 1

    /api/age-structure-diff/{SA4 Code}/{ Sex}/{ Year1}/{ Year2}, 
    / api / age - structure - diff / 102 / 1 / 2011 / 2016 
    should return the difference in age between 2016 and 2011 Census Years


    /api/age-structure/1/1 (This will summarise all SA4s in NSW)
    Try to also support passing a STATE CODE instead of a SA4 Code for the above end points. 
    This should aggreate the SA4s ex. 
    
    The following properties should be included in the JSON object:
    Region Code (ex. SA4 code or STATE code)
    Region name
    Age
    Sex (ex. Males/Females/Persons)
    Population

    
SEX_ABS,Sex,    AGE,    Age,        STATE,  State,              REGIONTYPE, Geography Level,            ASGS_2016,      Region,         TIME,   Census year,    Value,  Flag Codes,   Flags
1,      Males,  TT,     All ages    ,1,     New South Wales,    SA4,        Statistical Area Level 4,   102,            Central Coast,  2011,   2011,           150704  ,               ,
1,      Males,  TT,     All ages,    1,     New South Wales,    SA4,        Statistical Area Level 4,   102,            Central Coast,  2016,   2016,           158768, ,
2,      Females,TT,     All ages,    1,     New South Wales,    SA4,        Statistical Area Level 4,   102,            Central Coast,  2011,   2011,           161484, ,




    */




    public class Age
    {
    }
}