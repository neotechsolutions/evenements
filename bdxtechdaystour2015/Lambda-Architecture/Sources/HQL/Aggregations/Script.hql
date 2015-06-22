FROM Raws
INSERT OVERWRITE TABLE Output
SELECT cast(concat(year, "-", 
		IF(length(month) < 2, concat( "0", month), month), "-", 
		IF(length(day) < 2, concat( "0", day), day), " ", 
		IF(length(hour) < 2, concat( "0", hour), hour), ":00:00")	
	as timestamp) as date, device, AVG(data)
GROUP BY year, month, day, hour, device, type;