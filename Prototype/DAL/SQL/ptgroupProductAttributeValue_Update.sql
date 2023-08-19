use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductAttributeValue_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductAttributeValue_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for ProductAttributeValue table.
-- =============================================
create procedure [dbo].[ptgroupProductAttributeValue_Update]
(
	@Id int,
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
	@PictureId int
)

as

set nocount on

update [ProductAttributeValue]
set [ProductId] = @ProductId,
	[AttributeId] = @AttributeId,
	[AttributeValueTypeId] = @AttributeValueTypeId,
	[AssociatedProductId] = @AssociatedProductId,
	[Name] = @Name,
	[ColorSquaresRgb] = @ColorSquaresRgb,
	[ImageSquaresPictureId] = @ImageSquaresPictureId,
	[PriceAdjustment] = @PriceAdjustment,
	[WeightAdjustment] = @WeightAdjustment,
	[Cost] = @Cost,
	[Quantity] = @Quantity,
	[IsPreSelected] = @IsPreSelected,
	[DisplayOrder] = @DisplayOrder,
	[PictureId] = @PictureId
where [Id] = @Id
go
