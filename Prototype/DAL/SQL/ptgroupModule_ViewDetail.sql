use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupModule_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupModule_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for Module table.
-- =============================================
create procedure [dbo].[ptgroupModule_ViewDetail]
(
	@ID int
)

as

set nocount on

select  [ID],
	[Name]
from [Module]
where [ID] = @ID
go
