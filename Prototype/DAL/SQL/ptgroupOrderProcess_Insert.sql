use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrderProcess_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrderProcess_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for OrderProcess table.
-- =============================================
create procedure [dbo].[ptgroupOrderProcess_Insert]
(
	@Id bigint,
	@OrderId int,
	@Status tinyint,
	@ProcessDate datetime,
	@EmployeeId int
)

as

set nocount on

insert into [OrderProcess]
(
	[Id],
	[OrderId],
	[Status],
	[ProcessDate],
	[EmployeeId]
)
values
(
	@Id,
	@OrderId,
	@Status,
	@ProcessDate,
	@EmployeeId
)
go
