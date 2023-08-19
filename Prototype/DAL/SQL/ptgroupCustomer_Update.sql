use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupCustomer_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupCustomer_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for Customers table.
-- =============================================
create procedure [dbo].[ptgroupCustomer_Update]
(
	@Id int,
	@Name nvarchar(250),
	@Address nvarchar(250),
	@Email nvarchar(250),
	@Phone nvarchar(50),
	@Country nvarchar(50),
	@IdGuid nvarchar(128)
)

as

set nocount on

update [Customers]
set [Name] = @Name,
	[Address] = @Address,
	[Email] = @Email,
	[Phone] = @Phone,
	[Country] = @Country,
	[IdGuid] = @IdGuid
where [Id] = @Id
go
