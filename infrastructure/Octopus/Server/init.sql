CREATE DATABASE octopus
GO

USE octopus
GO

CREATE LOGIN octopus WITH PASSWORD = 'N0tS3cr3t!'
GO

CREATE USER [octopus] FOR LOGIN [octopus]
GO

ALTER ROLE db_owner ADD MEMBER octopus
GO