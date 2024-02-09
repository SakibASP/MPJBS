 
 -- Author : Rocky

 -- EXEC getCollection

CREATE PROC getCollection
AS
BEGIN
 SELECT M.Name,C.PaidAmount,CONVERT(date,C.CollectionDate) CollectionDate 
 FROM Collection C
 JOIN Members M ON C.MemberId = M.Id
END