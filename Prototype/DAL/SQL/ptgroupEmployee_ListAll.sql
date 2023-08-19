use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupEmployee_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupEmployee_ListAll]
go

create procedure [dbo].[ptgroupEmployee_ListAll]

as

set nocount on

select  [Id],
	[FirstName],
	[LastName],
	[Address],
	[Phone],
	[PictureId],
	[IsActive],
	[CreatedDate],
	[ModifiedDate]
from [Employee] WITH (NOLOCK) where IsDel = 0
go
