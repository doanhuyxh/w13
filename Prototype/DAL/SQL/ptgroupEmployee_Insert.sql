use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupEmployee_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupEmployee_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for Employee table.
-- =============================================
create procedure [dbo].[ptgroupEmployee_Insert]
(
	@Id int output,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Address nvarchar(350),
	@Phone varchar(20),
	@PictureId int,
	@IsActive bit,
	@CreatedDate datetime
)

as

set nocount on

insert into [Employee]
(
	[Id],
	[FirstName],
	[LastName],
	[Address],
	[Phone],
	[PictureId],
	[IsActive],
	[CreatedDate]
)
values
(
	@Id,
	@FirstName,
	@LastName,
	@Address,
	@Phone,
	@PictureId,
	@IsActive,
	@CreatedDate
)
set @Id = SCOPE_IDENTITY()
go
