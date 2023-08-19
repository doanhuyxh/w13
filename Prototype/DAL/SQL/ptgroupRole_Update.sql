use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupRole_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupRole_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for Role table.
-- =============================================
create procedure [dbo].[ptgroupRole_Update]
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

update [Role]
set [Add] = @Add,
	[Edit] = @Edit,
	[View] = @View,
	[Delete] = @Delete,
	[Import] = @Import,
	[Export] = @Export,
	[Upload] = @Upload,
	[Publish] = @Publish,
	[Report] = @Report,
	[Print] = @Print
where [UserId] = @UserId	and [ModuleId] = @ModuleId
go
