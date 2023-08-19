use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupCustomer_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupCustomer_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for Customers table.
-- =============================================
create procedure [dbo].[ptgroupCustomer_Insert]
(
	@Name nvarchar(250),
	@Address nvarchar(250),
	@Email nvarchar(250),
	@Phone nvarchar(50),
	@Country nvarchar(50),
	@IdGuid nvarchar(128),
	@Id int OUTPUT
)

as

set nocount on

insert into [Customers]
(
	[Name],
	[Address],
	[Email],
	[Phone],
	[Country],
	[IdGuid]
)
values
(
	@Name,
	@Address,
	@Email,
	@Phone,
	@Country,
	@IdGuid
)

set @Id = scope_identity()
go
