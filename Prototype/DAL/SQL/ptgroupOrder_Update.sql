use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrder_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrder_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for Orders table.
-- =============================================
create procedure [dbo].[ptgroupOrder_Update]
(
	@Id int,
	@CustomerId int,
	@OrderDate datetime,
	@Status nvarchar(50)
)

as

set nocount on

update [Orders]
set [CustomerId] = @CustomerId,
	[OrderDate] = @OrderDate,
	[Status] = @Status
where [Id] = @Id
go
