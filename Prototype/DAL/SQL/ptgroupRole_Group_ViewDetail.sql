use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupRole_Group_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupRole_Group_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for Role_Group table.
-- =============================================
create procedure [dbo].[ptgroupRole_Group_ViewDetail]
(
	@ID int
)

as

set nocount on

select  [ID],
	[Name]
from [Role_Group]
where [ID] = @ID
go
