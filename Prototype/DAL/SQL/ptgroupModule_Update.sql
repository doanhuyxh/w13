use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupModule_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupModule_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for Module table.
-- =============================================
create procedure [dbo].[ptgroupModule_Update]
(
	@ID int,
	@Name nvarchar(250)
)

as

set nocount on

update [Module]
set [Name] = @Name
where [ID] = @ID
go
