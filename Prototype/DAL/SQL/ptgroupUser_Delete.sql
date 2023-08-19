use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupUser_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupUser_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for Users table.
-- =============================================
create procedure [dbo].[ptgroupUser_Delete]
(
	@Id int
)

as

set nocount on

delete from [Users]
where [Id] = @Id
go
