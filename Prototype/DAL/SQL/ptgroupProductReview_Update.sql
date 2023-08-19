use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductReview_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductReview_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for ProductReview table.
-- =============================================
create procedure [dbo].[ptgroupProductReview_Update]
(
	@Id bigint,
	@CustomerId int,
	@ProductId bigint,
	@IsApproved bit,
	@Title nvarchar(250),
	@ReviewText nvarchar(max),
	@Rating int,
	@CreatedOn datetime
)

as

set nocount on

update [ProductReview]
set [CustomerId] = @CustomerId,
	[ProductId] = @ProductId,
	[IsApproved] = @IsApproved,
	[Title] = @Title,
	[ReviewText] = @ReviewText,
	[Rating] = @Rating,
	[CreatedOn] = @CreatedOn
where [Id] = @Id
go
