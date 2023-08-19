use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupRole_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupRole_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for Role table.
-- =============================================
create procedure [dbo].[ptgroupRole_Insert]
(
	@UserId int,
	@ModuleId int,
	@Add bit,
	@Edit bit,
	@View bit,
	@Delete bit,
	@Import bit,
	@Export bit,
	@Upload bit,
	@Publish bit,
	@Report bit,
	@Print bit
)

as

set nocount on

insert into [Role]
(
	[UserId],
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
)
values
(
	@UserId,
	@ModuleId,
	@Add,
	@Edit,
	@View,
	@Delete,
	@Import,
	@Export,
	@Upload,
	@Publish,
	@Report,
	@Print
)
go
