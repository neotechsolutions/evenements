CREATE PROCEDURE [dbo].[IngressAggregations]
    @Aggregations dbo.[AggregationType] READONLY
AS
BEGIN
    BEGIN TRAN
		MERGE [dbo].[Aggregation] AS target
        USING (SELECT a.[Date], a.[Device], a.[Value] FROM @Aggregations a) AS source ([Date], [Device], [Value])
		ON (target.Date = source.Date AND target.Device = source.Device)
		WHEN MATCHED AND target.Value <> source.Value THEN
			UPDATE SET target.Value = source.Value
		WHEN NOT MATCHED THEN
			INSERT ([Device], [Date], [Value])
			VALUES (source.Device, source.Date, source.Value);
    COMMIT TRAN
END
GO