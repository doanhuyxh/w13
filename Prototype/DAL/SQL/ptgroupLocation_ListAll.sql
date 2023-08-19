use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupLocation_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupLocation_ListAll]
go

create procedure [dbo].[ptgroupLocation_ListAll]

as

set nocount on

select  [Id],
	[Name],
	[ParentId],
	[CreatedDate],
	[ModifiedDate],
	[SlugName],
	[lat],
	[lng]
from [Location] WITH (NOLOCK)
go
