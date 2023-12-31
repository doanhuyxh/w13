USE [PhucThanh_Ecomerce]
GO
/****** Object:  StoredProcedure [dbo].[CustomerOrders_ListAllPaging]    Script Date: 4/20/2017 12:22:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDG
-- Create date: <Create Date,,>
-- Edit By: Minh Duong
-- Modified date: 2/15/2017
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[CustomerOrders_ListAllPaging]
	(
		@CustomerId int,
		@Status int = 0,
		@dateFrom datetime = null,
		@dateTo datetime = null,
		@pageIndex int,
		@pageSize int,
		@totalRow int output
	)

		as

		set nocount on

		DECLARE @UpperBand int, @LowerBand int

		SELECT @totalRow = COUNT(*) FROM Orders where CustomerId = @CustomerId

		SET @LowerBand  = (@pageIndex - 1) * @PageSize
		SET @UpperBand  = (@pageIndex * @PageSize)

		SELECT  [Id],
			[OrderCode],
			[CustomerId],
			[OrderDate],
			[Status],
			[TotalPrice]
		 FROM (SELECT  o.Id,o.CustomerId, o.OrderDate, o.[Status],o.[OrderCode],(select Sum(UnitPrice * Quantity) from OrderDetails where OrderId = o.Id) as TotalPrice,
		 ROW_NUMBER() OVER(ORDER BY o.Id DESC) AS RowNumber FROM Orders o WITH (NOLOCK)
		 where CustomerId = @CustomerId and (@dateFrom is null or (@dateFrom is not null and convert(date, OrderDate) >= convert(date, @dateFrom) ))
				and (@dateTo is null or (@dateTo is not null and CONVERT(date,OrderDate) <= CONVERT(date,@dateTo)))
		 ) AS temp
		WHERE RowNumber > @LowerBand AND RowNumber <= @UpperBand

