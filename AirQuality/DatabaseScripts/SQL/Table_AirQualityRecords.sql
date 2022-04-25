/****** Object:  Table [dbo].[AirQualityRecords]    Script Date: 25/04/2022 2:04:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirQualityRecords](
	[id] [nvarchar](5) NOT NULL,
	[country] [nvarchar](50) NULL,
	[state] [nvarchar](150) NULL,
	[city] [nvarchar](150) NULL,
	[station] [nvarchar](255) NULL,
	[last_update] [nvarchar](20) NULL,
	[pollutant_id] [nvarchar](5) NULL,
	[pollutant_min] [nvarchar](5) NULL,
	[pollutant_max] [nvarchar](5) NULL,
	[pollutant_avg] [nvarchar](5) NULL,
	[pollutant_unit] [nvarchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AirQualityRecords_City]    Script Date: 25/04/2022 2:04:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_AirQualityRecords_City] ON [dbo].[AirQualityRecords]
(
	[city] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AirQualityRecords_State]    Script Date: 25/04/2022 2:04:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_AirQualityRecords_State] ON [dbo].[AirQualityRecords]
(
	[state] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
