use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupAddress_Received_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupAddress_Received_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure update a new record for Address_Received table.
-- =============================================
create procedure [dbo].[ptgroupAddress_Received_Update]
(
	@Id int,
	@CustomerId int,
	@FullName nvarchar(350),
	@Phone varchar(50),
	@Email nvarchar(500),
	@CityId int,
	@Address nvarchar(500),
	@Note nvarchar(2000),
	@CreatedDate datetime
)

as

set nocount on

update [Address_Received]
set [CustomerId] = @CustomerId,
	[FullName] = @FullName,
	[Phone] = @Phone,
	[Email] = @Email,
	[CityId] = @CityId,
	[Address] = @Address,
	[Note] = @Note,
	[CreatedDate] = @CreatedDate
where [Id] = @Id
go
