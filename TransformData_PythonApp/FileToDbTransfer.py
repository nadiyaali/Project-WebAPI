#This program will read the file, filter its contents and transfer them to a MS SQL Server DB
import os

print("This script will trasfer data from CSV file to MS SQL Server Database ABS_DATA")
print("File should have the column names : SEX_ABS,Sex,AGE,Age,STATE,State,REGIONTYPE,Geography Level,ASGS_2016,Region,TIME,Census year,Value,Flag Codes,Flags")
print("Place CSV file in the 'Data' folder")


#file name : ABS_C16_T01_TS_SA_08062021164508583.csv
# Try to open the file
try:
    fname = input("Enter the name of the CSV file : ")
    #fname = "ABS_C16_T01_TS_SA_08062021164508583.csv"
    #Construct full path with file name
    filename = "Data/"+ fname
    open(filename, 'r')
except FileNotFoundError:
    print("File not found.")
    #exit()


import pandas as pd # data processing, CSV file I/O (e.g. pd.read_csv)

# Read the file
#data = pd.read_csv(r"Data/ABS_C16_T01_TS_SA_08062021164508583.csv", dtype=str)
data= pd.read_csv(fr'{filename}',dtype=str )

# Replace white spaces by underscores
data.columns = [c.replace(' ', '_') for c in data]
print(data.head())

#Funcation for converting DataSries to DataFrame
def convertSeriesToList(series):
    indexlist = []
    for index, v in series.items():
        #print('index: ', index, 'value: ', v)        
        indexlist.append(index)
    return indexlist

#Filter data for STATE table
filDataState = data[['STATE', 'State']].value_counts()
indexlistState = convertSeriesToList(filDataState)
dfState = pd.DataFrame(indexlistState,columns=['STATE', 'State'])
print(dfState.head())

#Filter data for REGION table
filDataRegion = data[['ASGS_2016', 'Region','Geography_Level','REGIONTYPE']].value_counts()
indexlistRegion = convertSeriesToList(filDataRegion)
dfRegion = pd.DataFrame(indexlistRegion,columns=['ASGS_2016', 'Region','Geography_Level','REGIONTYPE'])
print(dfRegion.head())

#Filter data for AGE Table
filDataAge = data[['AGE', 'Age']].value_counts()
indexlistAge = convertSeriesToList(filDataAge)
dfAge = pd.DataFrame(indexlistAge,columns=['AGE', 'Age'])
print(dfAge.head())

# Dropped the columns Geography_Level,Flag_Codes, Flags
# These columns  are used
# SEX_ABS, Sex,   AGE,   STATE,    REGIONTYPE,    ASGS_2016,   TIME,   Census year,  Value
filDataAll = data[['SEX_ABS','Sex','AGE','STATE','REGIONTYPE','ASGS_2016','TIME','Census_year','Value']]
#print("TYPE OF RESULT : ",type(filDataAll))
print(filDataAll.head())

# Now insert this filtered data into database tables
import pyodbc 

conn = pyodbc.connect('Driver={SQL Server};'
                      'Server=DESKTOP-V2RFM0L;'
                      'Database=ABS_DATA;'
                      'Trusted_Connection=yes;')
cursor = conn.cursor()


for index, row in dfState.iterrows():
     cursor.execute("INSERT INTO DIM_STATE (STATE, STATE_TEXT) values(?,?)", row.STATE, row.State)

for index, row in dfAge.iterrows():
     cursor.execute("INSERT INTO DIM_AGE (AGE, AGE_TEXT) values(?,?)", row.AGE, row.Age)

for index, row in dfRegion.iterrows():
    cursor.execute("INSERT INTO DIM_REGION (REGION, REGION_TEXT, REGION_TYPE, REGION_TYPE_CODE) values(?,?,?,?)", row.ASGS_2016, row.Region, row.Geography_Level, row.REGIONTYPE)


for index, row in filDataAll.iterrows():
  cursor.execute("INSERT INTO FACT_POPULATION values(?,?,?,?,?,?,?,?,?,?)",
                 index,row.SEX_ABS,row.Sex,row.AGE,row.STATE,row.ASGS_2016,row.REGIONTYPE,row.TIME,row.Census_year,row.Value)
   

conn.commit()
conn.close()

print("Data transfer has been completed")



