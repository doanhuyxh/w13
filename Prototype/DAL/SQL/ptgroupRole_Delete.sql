use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupRole_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupRole_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for Role table.
-- =============================================
create procedure [dbo].[ptgroupRole_Delete]
(
	@GroupId int,
	@ModuleId int
)

as

set nocount on

delete from [Role]
where [GroupId] = @GroupId
	and [ModuleId] = @ModuleId
go
