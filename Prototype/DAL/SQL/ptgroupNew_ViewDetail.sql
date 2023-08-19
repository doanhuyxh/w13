use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupNew_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupNew_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for News table.
-- =============================================
create procedure [dbo].[ptgroupNew_ViewDetail]
(
	@Id bigint
)

as

set nocount on

select  [Id],
	[Title],
	[Description],
	[Content],
	[ImageUrl],
	[MetaKeyword],
	[MetaDescription],
	[TitleSeo],
	[IsHot],
	[IsDel],
	[Published],
	[CreatedDate],
	[ModifiedDate]
from [News]
where [Id] = @Id
go
