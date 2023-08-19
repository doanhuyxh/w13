use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupRole_Group_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupRole_Group_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for Role_Group table.
-- =============================================
create procedure [dbo].[ptgroupRole_Group_Delete]
(
	@ID int
)

as

set nocount on

delete from [Role_Group]
where [ID] = @ID
go
