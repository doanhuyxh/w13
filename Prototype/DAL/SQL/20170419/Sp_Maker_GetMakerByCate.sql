-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		minh duong
-- Create date: 2017/04/19
-- Description:	Get list maker with category Id
-- =============================================
CREATE PROCEDURE [dbo].[Sp_Maker_GetMakerByCate] 
	@cateId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT	m.MakerName, m.Id
	FROM 	Maker m		inner join 
			Products	p on m.Id = p.MakerId
						left join ProductCate pc on p.Id = pc.[ProductID]
	WHERE p.CateID = @cateId or pc.CateID = @cateId
	GROUP BY m.MakerName, m.Id
END
GO
