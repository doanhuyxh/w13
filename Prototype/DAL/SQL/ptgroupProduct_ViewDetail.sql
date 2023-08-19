use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProduct_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProduct_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for Products table.
-- =============================================
create procedure [dbo].[ptgroupProduct_ViewDetail]
(
	@Id bigint
)

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
from [Products]
where [Id] = @Id
go
