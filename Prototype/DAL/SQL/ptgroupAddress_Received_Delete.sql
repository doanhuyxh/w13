use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupAddress_Received_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupAddress_Received_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure delete a new record for Address_Received table.
-- =============================================
create procedure [dbo].[ptgroupAddress_Received_Delete]
(
	@Id int
)

as

set nocount on

delete from [Address_Received]
where [Id] = @Id
go
