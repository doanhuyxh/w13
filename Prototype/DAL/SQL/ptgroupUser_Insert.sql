use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupUser_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupUser_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for Users table.
-- =============================================
create procedure [dbo].[ptgroupUser_Insert]
(
	@Id int output,
	@UserName nvarchar(50),
	@Password nvarchar(350),
	@EmployeeId int
)

as

set nocount on

insert into [Users]
(
	[UserName],
	[Password],
	[EmployeeId]
)
values
(
	@UserName,
	@Password,
	@EmployeeId
)
set @Id = SCOPE_IDENTITY()
go
