declare @companyId int
declare @company varchar(max)

set @company = 'EMP Demo'
set @companyId = 1047

declare @testCompany varchar(max)
set @testCompany = (select company from tblEA_Companies where CompanyID = @companyId)

if(@company != @testCompany)		
	THROW 51000, 'Did you change the company name', 1; 



--update EMP_Demo.dbo.tblEmployee
--set username = email from EMPAuthentication.dbo.tblEA_Users u
--where u.eid = EMP_Demo.dbo.tblEmployee.EID and u.CompanyId = @companyId