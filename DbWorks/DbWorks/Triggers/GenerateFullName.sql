ALTER TABLE dbo.Customers DROP COLUMN FullName  
ALTER TABLE dbo.Customers ADD FullName AS (FirstName + ' ' + LastName)