CREATE TYPE [dbo].[AggregationType] AS TABLE
(
	[Date]			 SMALLDATETIME  NOT NULL,
	[Device]	     nvarchar(255) NOT NULL,
	[Value]          DECIMAL (18, 3) NOT NULL
)