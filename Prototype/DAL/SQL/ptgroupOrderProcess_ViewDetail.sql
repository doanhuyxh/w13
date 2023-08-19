use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrderProcess_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrderProcess_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for OrderProcess table.
-- =============================================
create procedure [dbo].[ptgroupOrderProcess_ViewDetail]
(
	@Id bigint
)

as

set nocount on

select  [Id],
	[OrderId],
	[Status],
	[ProcessDate],
	[EmployeeId]
from [OrderProcess]
where [Id] = @Id
go
