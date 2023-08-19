use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupUser_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupUser_ListAll]
go

create procedure [dbo].[ptgroupUser_ListAll]

as

set nocount on

select  [Id],
	[UserName],
	[Password],
	[LastPass],
	[RoleId],
	[EmployeeId],
	[CreatedDate],
	[ModifiedDate],
	[LastLogin],
	[Token]
from [Users] WITH (NOLOCK)
go
