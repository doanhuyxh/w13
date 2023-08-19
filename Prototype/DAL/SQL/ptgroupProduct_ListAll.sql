use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProduct_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProduct_ListAll]
go

create procedure [dbo].[ptgroupProduct_ListAll]

as

set nocount on

select  [Id],
	[MakerId],
	[CateId],
	[ProductName],
	[ProductCode],
	[Description],
	[Images],
	[PictureId],
	[UnitPrice],
	[SellPrice],
	[Quantity],
	[Views],
	[Sort],
	[Published],
	[MetaKeyword],
	[MetaDescription],
	[CreatedDate],
	[LastmodifiedDate],
	[lat],
	[lng]
from [Products] WITH (NOLOCK)
go
