use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductAttributeValue_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductAttributeValue_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for ProductAttributeValue table.
-- =============================================
create procedure [dbo].[ptgroupProductAttributeValue_ViewDetail]
(
	@Id int
)

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
from [ProductAttributeValue]
where [Id] = @Id
go
