use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrder_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrder_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for Orders table.
-- =============================================
create procedure [dbo].[ptgroupOrder_Insert]
(
	@CustomerId int,
	@OrderDate datetime,
	@Status nvarchar(50),
	@Id int OUTPUT
)

as

set nocount on

insert into [Orders]
(
	[CustomerId],
	[OrderDate],
	[Status]
)
values
(
	@CustomerId,
	@OrderDate,
	@Status
)

set @Id = scope_identity()
go
