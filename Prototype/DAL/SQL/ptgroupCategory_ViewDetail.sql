use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupCategory_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupCategory_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for Category table.
-- =============================================
create procedure [dbo].[ptgroupCategory_ViewDetail]
(
	@Id int
)

as

set nocount on

select  [Id],
	[Name],
	[ParentId],
	[TitleSeo],
	[KeywordSeo],
	[Description],
	[Sort],
	[IsDel],
	[PositionMenu],
	[PictureId],
	[CreatedDate],
	[ModifiedDate]
from [Category]
where [Id] = @Id
go
