use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupLocation_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupLocation_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure view detail a new record for Location table.
-- =============================================
create procedure [dbo].[ptgroupLocation_ViewDetail]
(
	@Id int
)

as

set nocount on

select  [Id],
	[Name],
	[ParentId],
	[CreatedDate],
	[ModifiedDate],
	[SlugName],
	[lat],
	[lng]
from [Location]
where [Id] = @Id
go
