use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductAttributeValue_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductAttributeValue_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for ProductAttributeValue table.
-- =============================================
create procedure [dbo].[ptgroupProductAttributeValue_Insert]
(
	@ProductId bigint,
	@AttributeId int,
	@AttributeValueTypeId int,
	@AssociatedProductId int,
	@Name nvarchar(400),
	@ColorSquaresRgb nvarchar(100),
	@ImageSquaresPictureId int,
	@PriceAdjustment decimal(18, 4),
	@WeightAdjustment decimal(18, 4),
	@Cost decimal(18, 4),
	@Quantity int,
	@IsPreSelected bit,
	@DisplayOrder int,
	@PictureId int,
	@Id int OUTPUT
)

as

set nocount on

insert into [ProductAttributeValue]
(
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
)
values
(
	@ProductId,
	@AttributeId,
	@AttributeValueTypeId,
	@AssociatedProductId,
	@Name,
	@ColorSquaresRgb,
	@ImageSquaresPictureId,
	@PriceAdjustment,
	@WeightAdjustment,
	@Cost,
	@Quantity,
	@IsPreSelected,
	@DisplayOrder,
	@PictureId
)

set @Id = scope_identity()
go
