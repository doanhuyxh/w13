use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupAddress_Received_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupAddress_Received_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure insert a new record for Address_Received table.
-- =============================================
create procedure [dbo].[ptgroupAddress_Received_Insert]
(
	@CustomerId int,
	@FullName nvarchar(350),
	@Phone varchar(50),
	@Email nvarchar(500),
	@CityId int,
	@Address nvarchar(500),
	@Note nvarchar(2000),
	@CreatedDate datetime,
	@Id int OUTPUT
)

as

set nocount on

insert into [Address_Received]
(
	[CustomerId],
	[FullName],
	[Phone],
	[Email],
	[CityId],
	[Address],
	[Note],
	[CreatedDate]
)
values
(
	@CustomerId,
	@FullName,
	@Phone,
	@Email,
	@CityId,
	@Address,
	@Note,
	@CreatedDate
)

set @Id = scope_identity()
go
