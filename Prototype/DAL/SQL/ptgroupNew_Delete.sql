use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupNew_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupNew_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for News table.
-- =============================================
create procedure [dbo].[ptgroupNew_Delete]
(
	@Id bigint
)

as

set nocount on

delete from [News]
where [Id] = @Id
go
