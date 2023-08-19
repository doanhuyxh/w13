use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupModule_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupModule_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for Module table.
-- =============================================
create procedure [dbo].[ptgroupModule_Delete]
(
	@ID int
)

as

set nocount on

delete from [Module]
where [ID] = @ID
go
