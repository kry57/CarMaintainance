use Car_Maintainance 
DELETE FROM Products
DBCC CHECKIDENT ('Products', RESEED, 0)