use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupUser_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupUser_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure update a new record for Users table.
-- =============================================
create procedure [dbo].[ptgroupUser_Update]
(
	@Id int,
	@UserName nvarchar(50),
	@Password nvarchar(350),
	@LastPass nvarchar(350),
	@RoleId int,
	@EmployeeId int,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@LastLogin datetime,
	@Token varchar(500)
)

as

set nocount on

update [Users]
set [UserName] = @UserName,
	[Password] = @Password,
	[LastPass] = @LastPass,
	[RoleId] = @RoleId,
	[EmployeeId] = @EmployeeId,
	[CreatedDate] = @CreatedDate,
	[ModifiedDate] = @ModifiedDate,
	[LastLogin] = @LastLogin,
	[Token] = @Token
where [Id] = @Id
go
