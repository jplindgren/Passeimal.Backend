select Id, Name, Vicinity, Latitude, Longitude, PukePoints 
 from ( SELECT ROW_NUMBER()  OVER ( ORDER BY pl.Relevance desc) AS RowNum, 
 pl.Id, pl.Name, pl.Vicinity, pl.Latitude, pl.Longitude, COALESCE(sum(pu.Severity),0) as pukePoints 
 FROM Places pl left join Pukes pu on pu.Place_Id = pl.Id 
 WHERE Latitude > -22.923015370275447 and Latitude < -22.887064629724552 and Longitude > -43.195614028538259 and Longitude < -43.156585971461766 
 group by pl.Id, pl.Name, pl.Vicinity, pl.Latitude, pl.Longitude, pl.Relevance) AS result 
WHERE   RowNum >= 0 AND RowNum < 20 ORDER BY RowNum