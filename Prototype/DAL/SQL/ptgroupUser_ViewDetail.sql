use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupUser_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupUser_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for Users table.
-- =============================================
create procedure [dbo].[ptgroupUser_ViewDetail]
(
	@Id int
)

as

set nocount on

select  u.[Id],
	[UserName],
	[Password],
	[LastPass],
	[RoleId],
	[EmployeeId],
	[LastLogin],
	[Token],
	e.[Address],e.FirstName,e.LastName,e.IsActive,e.Phone
from [Users] u left join [Employee] e on u.EmployeeId = e.Id
where u.[Id] = @Id
go
