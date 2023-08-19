use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupMaker_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupMaker_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for Maker table.
-- =============================================
create procedure [dbo].[ptgroupMaker_ViewDetail]
(
	@Id int
)

as

set nocount on

select  [Id],
	[MakerName],
	[Address],
	[Phone],
	[IsActive],
	[CreatedDate],
	[ModifiedDate]
from [Maker]
where [Id] = @Id
go
