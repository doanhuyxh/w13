use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrderProcess_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrderProcess_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for OrderProcess table.
-- =============================================
create procedure [dbo].[ptgroupOrderProcess_Update]
(
	@Id bigint,
	@OrderId int,
	@Status tinyint,
	@ProcessDate datetime,
	@EmployeeId int
)

as

set nocount on

update [OrderProcess]
set [OrderId] = @OrderId,
	[Status] = @Status,
	[ProcessDate] = @ProcessDate,
	[EmployeeId] = @EmployeeId
where [Id] = @Id
go
