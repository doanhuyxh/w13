use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupModule_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupModule_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for Module table.
-- =============================================
create procedure [dbo].[ptgroupModule_Insert]
(
	@Name nvarchar(250),
	@ID int OUTPUT
)

as

set nocount on

if (exists (select top 1 ID from Module where Name = @Name))
BEGIN
	set @ID = -1
END
ELSE
BEGIN

insert into [Module]
(
	[Name]
)
values
(
	@Name
)

set @ID = scope_identity()
END
go
