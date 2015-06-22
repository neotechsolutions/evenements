DROP TABLE IF EXISTS Raws;
CREATE EXTERNAL TABLE Raws (
    year STRING,
    month STRING,
    day STRING,
    hour STRING,
    device STRING,
    type STRING,
    data DECIMAL(18,3)
)
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' LINES TERMINATED BY '\n'
STORED AS TEXTFILE LOCATION "wasb://data@mattk.blob.core.windows.net/events_/"
TBLPROPERTIES ("skip.header.line.count"="1");

DROP TABLE IF EXISTS Output;
CREATE TABLE Output (
    date STRING,
    device STRING,
    value DECIMAL(18,3)
)
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' LINES TERMINATED BY '\n'
STORED AS TEXTFILE LOCATION "wasb://aggregations@mattk.blob.core.windows.net/output/";
