use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductReview_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductReview_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for ProductReview table.
-- =============================================
create procedure [dbo].[ptgroupProductReview_ViewDetail]
(
	@Id bigint
)

as

set nocount on

select  [Id],
	[CustomerId],
	[ProductId],
	[IsApproved],
	[Title],
	[ReviewText],
	[Rating],
	[CreatedOn]
from [ProductReview]
where [Id] = @Id
go
