use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupRole_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupRole_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for Role table.
-- =============================================
create procedure [dbo].[ptgroupRole_ViewDetail]
(
	@UserId int,
	@ModuleId int
)

as

set nocount on

select  UserId,
	[ModuleId],
	[Add],
	[Edit],
	[View],
	[Delete],
	[Import],
	[Export],
	[Upload],
	[Publish],
	[Report],
	[Print]
from [Role]
where UserId = @UserId
	and [ModuleId] = @ModuleId
go
