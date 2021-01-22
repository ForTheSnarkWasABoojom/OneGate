CREATE DATABASE teamcity
GO

USE teamcity
GO

CREATE LOGIN teamcity WITH PASSWORD = 'N0tS3cr3t!'
GO

CREATE USER [teamcity] FOR LOGIN [teamcity]
GO

ALTER ROLE db_owner ADD MEMBER teamcity
GO