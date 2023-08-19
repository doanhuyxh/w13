use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProduct_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProduct_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for Products table.
-- =============================================
create procedure [dbo].[ptgroupProduct_Insert]
(
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
	@lng varchar(50),
	@Id bigint OUTPUT
)

as

set nocount on

insert into [Products]
(
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
)
values
(
	@MakerId,
	@CateId,
	@ProductName,
	@ProductCode,
	@Description,
	@Images,
	@PictureId,
	@UnitPrice,
	@SellPrice,
	@Quantity,
	@Views,
	@Sort,
	@Published,
	@MetaKeyword,
	@MetaDescription,
	@CreatedDate,
	@LastmodifiedDate,
	@lat,
	@lng
)

set @Id = scope_identity()
go
