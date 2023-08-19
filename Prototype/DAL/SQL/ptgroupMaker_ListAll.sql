use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupMaker_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupMaker_ListAll]
go

create procedure [dbo].[ptgroupMaker_ListAll]

as

set nocount on

select  [Id],
	[MakerName],
	[Address],
	[Phone],
	[IsActive],
	[CreatedDate],
	[ModifiedDate]
from [Maker] WITH (NOLOCK)
go
