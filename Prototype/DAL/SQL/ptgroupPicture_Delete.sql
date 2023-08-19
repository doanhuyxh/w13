use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPicture_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPicture_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for Picture table.
-- =============================================
create procedure [dbo].[ptgroupPicture_Delete]
(
	@Id bigint
)

as

set nocount on

delete from [Picture]
where [Id] = @Id
go
