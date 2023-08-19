use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupEmployee_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupEmployee_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for Employee table.
-- =============================================
create procedure [dbo].[ptgroupEmployee_Delete]
(
	@Id int
)

as

set nocount on

update [Employee] set IsDel = 1
where [Id] = @Id
go
