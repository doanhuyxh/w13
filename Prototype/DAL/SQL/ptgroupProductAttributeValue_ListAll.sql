use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductAttributeValue_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductAttributeValue_ListAll]
go

create procedure [dbo].[ptgroupProductAttributeValue_ListAll]

as

set nocount on

select  [Id],
	[ProductId],
	[AttributeId],
	[AttributeValueTypeId],
	[AssociatedProductId],
	[Name],
	[ColorSquaresRgb],
	[ImageSquaresPictureId],
	[PriceAdjustment],
	[WeightAdjustment],
	[Cost],
	[Quantity],
	[IsPreSelected],
	[DisplayOrder],
	[PictureId]
from [ProductAttributeValue] WITH (NOLOCK)
go
