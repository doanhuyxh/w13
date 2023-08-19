use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductReview_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductReview_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for ProductReview table.
-- =============================================
create procedure [dbo].[ptgroupProductReview_Delete]
(
	@Id bigint
)

as

set nocount on

delete from [ProductReview]
where [Id] = @Id
go
