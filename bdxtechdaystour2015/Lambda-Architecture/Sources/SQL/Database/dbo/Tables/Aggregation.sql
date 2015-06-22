CREATE TABLE [dbo].[Aggregation]
(
	[Device]	     nvarchar(255) NOT NULL,
	[Date]			 SMALLDATETIME  NOT NULL,
	[Value]          DECIMAL (18, 3) NOT NULL,
	CONSTRAINT [PK_Aggregations] PRIMARY KEY CLUSTERED ([Device], [Date])
)