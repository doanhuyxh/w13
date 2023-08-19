use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupEmployee_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupEmployee_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for Employee table.
-- =============================================
create procedure [dbo].[ptgroupEmployee_Update]
(
	@Id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Address nvarchar(350),
	@Phone varchar(20),
	@PictureId int,
	@IsActive bit,
	@ModifiedDate datetime
)

as

set nocount on

update [Employee]
set [FirstName] = @FirstName,
	[LastName] = @LastName,
	[Address] = @Address,
	[Phone] = @Phone,
	[PictureId] = @PictureId,
	[IsActive] = @IsActive,
	[ModifiedDate] = @ModifiedDate
where [Id] = @Id
go
