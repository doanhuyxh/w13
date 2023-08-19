use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductReview_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductReview_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for ProductReview table.
-- =============================================
create procedure [dbo].[ptgroupProductReview_Insert]
(
	@CustomerId int,
	@ProductId bigint,
	@IsApproved bit,
	@Title nvarchar(250),
	@ReviewText nvarchar(max),
	@Rating int,
	@CreatedOn datetime,
	@Id bigint OUTPUT
)

as

set nocount on

insert into [ProductReview]
(
	[CustomerId],
	[ProductId],
	[IsApproved],
	[Title],
	[ReviewText],
	[Rating],
	[CreatedOn]
)
values
(
	@CustomerId,
	@ProductId,
	@IsApproved,
	@Title,
	@ReviewText,
	@Rating,
	@CreatedOn
)

set @Id = scope_identity()
go
