


SET IDENTITY_INSERT [MenuItem] ON
INSERT INTO MenuItem([MenuId],[MenuName],[MenuUrl],[MenuParentId],[Active]) VALUES (1,N'Info',N'#',null,1)
INSERT INTO MenuItem([MenuId],[MenuName],[MenuUrl],[MenuParentId],[Active]) VALUES (2,N'Activity',N'#',null,1)
INSERT INTO MenuItem([MenuId],[MenuName],[MenuUrl],[MenuParentId],[Active]) VALUES (3,N'Admin',N'#',null,1)
SET IDENTITY_INSERT [MenuItem] OFF

SET IDENTITY_INSERT [MenuItem] ON
INSERT INTO MenuItem([MenuId],[MenuName],[MenuUrl],[MenuParentId],[Active]) VALUES (101,N'Members',N'/Members/Index',1,1)
INSERT INTO MenuItem([MenuId],[MenuName],[MenuUrl],[MenuParentId],[Active]) VALUES (102,N'Types',N'/MemberTypes/Index',1,1)
SET IDENTITY_INSERT [MenuItem] OFF

SET IDENTITY_INSERT [MenuItem] ON
INSERT INTO MenuItem([MenuId],[MenuName],[MenuUrl],[MenuParentId],[Active]) VALUES (103,N'Collection',N'/Collections/Index',2,1)
INSERT INTO MenuItem([MenuId],[MenuName],[MenuUrl],[MenuParentId],[Active]) VALUES (104,N'Expense',N'/Expenses/Index',2,1)
SET IDENTITY_INSERT [MenuItem] OFF

SET IDENTITY_INSERT [MenuItem] ON
INSERT INTO MenuItem([MenuId],[MenuName],[MenuUrl],[MenuParentId],[Active]) VALUES (400,N'User Rights',N'/AdminRights/Index',3,1)
INSERT INTO MenuItem([MenuId],[MenuName],[MenuUrl],[MenuParentId],[Active]) VALUES (401,N'Maintain Users',N'/MaintainUsers/Index',3,1)
INSERT INTO MenuItem([MenuId],[MenuName],[MenuUrl],[MenuParentId],[Active]) VALUES (402,N'Maintain Roles',N'/RoleManager/Index',3,1)
INSERT INTO MenuItem([MenuId],[MenuName],[MenuUrl],[MenuParentId],[Active]) VALUES (403,N'Reset Password',N'/Identity/Account/ResetPassword',3,1)
SET IDENTITY_INSERT [MenuItem] OFF

----

GO


CREATE OR ALTER PROC [dbo].[usp_GetMenuData]
@UserId varchar(128)	--user id as input parameter
as
begin		
	select distinct mm.MenuID MID, mm.MenuName,mm.MenuURL,mm.MenuParentID
	--,ur.UserId,rm.Active 
	from 
	AspNetUserRoles ur				
	inner join MenuToRole rm on ur.RoleID=rm.RoleId				
	inner join MenuItem mm on mm.MenuId=rm.MenuID		
	inner join AspNetRoles r on r.Id =rm.RoleId				
	where (MenuParentId IN (Select MenuId from[dbo].[MenuItem] Where MenuName NOT IN ('MIS Reports','FIS Reports','Audit Reports','Validation Reports','Error Findings Tools'))
	OR MenuParentId IS NULL)
	and ur.UserId = @UserId  and rm.Active=1 and IsSelected=1	-- add more active condition if required	
end


---
GO

CREATE OR ALTER PROC [dbo].[usp_GetAllMenuData]
as
begin		
	select mm.MenuID MID, mm.MenuName,mm.MenuURL,mm.MenuParentID
	--,ur.UserId,rm.Active 
	from 
	AspNetUserRoles ur				
	inner join MenuToRole rm on ur.RoleID=rm.RoleId				
	inner join MenuItem mm on mm.MenuId=rm.MenuID		
	inner join AspNetRoles r on r.Id =rm.RoleId				
	where rm.Active=1	-- add more active condition if required	
end

GO

CREATE TABLE [WorkHistory](
[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
[Title] [nvarchar] (256) NOT NULL,
[WorkCode] [nvarchar] (128) NOT NULL,
[Details] [nvarchar] (MAX) NOT NULL,
[CreatedBy] [nvarchar](128) NULL,
[CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
[ModifiedBy] [nvarchar](128) NULL,
[ModifiedDate] [datetime] NULL
)

GO

DROP TABLE IF EXISTS [WorkImage]
CREATE TABLE [WorkImage](
[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
[WorkId] [int] NOT NULL REFERENCES WorkHistory(Id),
[IsCover] [bit] NULL,
[ImagePath] [nvarchar](256) NOT NULL,
[CreatedBy] [nvarchar](128) NULL,
[CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
[ModifiedBy] [nvarchar](128) NULL,
[ModifiedDate] [datetime] NULL
)