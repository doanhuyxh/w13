use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProduct_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProduct_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for Products table.
-- =============================================
create procedure [dbo].[ptgroupProduct_Update]
(
	@Id bigint,
	@MakerId int,
	@CateId int,
	@ProductName nvarchar(255),
	@ProductCode varchar(50),
	@Description nvarchar(255),
	@Images nvarchar(255),
	@PictureId bigint,
	@UnitPrice decimal(18, 0),
	@SellPrice decimal(18, 0),
	@Quantity int,
	@Views int,
	@Sort int,
	@Published bit,
	@MetaKeyword nvarchar(500),
	@MetaDescription nvarchar(500),
	@CreatedDate datetime,
	@LastmodifiedDate datetime,
	@lat varchar(50),
	@lng varchar(50)
)

as

set nocount on

update [Products]
set [MakerId] = @MakerId,
	[CateId] = @CateId,
	[ProductName] = @ProductName,
	[ProductCode] = @ProductCode,
	[Description] = @Description,
	[Images] = @Images,
	[PictureId] = @PictureId,
	[UnitPrice] = @UnitPrice,
	[SellPrice] = @SellPrice,
	[Quantity] = @Quantity,
	[Views] = @Views,
	[Sort] = @Sort,
	[Published] = @Published,
	[MetaKeyword] = @MetaKeyword,
	[MetaDescription] = @MetaDescription,
	[CreatedDate] = @CreatedDate,
	[LastmodifiedDate] = @LastmodifiedDate,
	[lat] = @lat,
	[lng] = @lng
where [Id] = @Id
go
