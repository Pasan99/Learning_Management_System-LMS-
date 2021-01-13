USE [RoadRunner]
GO

/****** Object:  Table [IMCloud].[BPMNSwimLane]    Script Date: 1/4/2021 6:46:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [IMCloud].[BPMNSwimLane](
	[BPMNSwimLaneId] [int] IDENTITY(1,1) NOT NULL,
	[ProcessId] [int] NOT NULL,
	[SwimLaneName] [varchar](50) NOT NULL,
	[SwimLaneX] [int] NOT NULL,
	[SwimLaneY] [int] NOT NULL,
	[SwimLaneWidth] [int] NOT NULL,
	[SwimLaneHeight] [int] NOT NULL,
 CONSTRAINT [PK_BPMNSwimLane] PRIMARY KEY CLUSTERED 
(
	[BPMNSwimLaneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [IMCloud].[BPMNSwimLane] ADD  CONSTRAINT [DF_Table_1_SwimLineName]  DEFAULT ('Swim Lane') FOR [SwimLaneName]
GO

ALTER TABLE [IMCloud].[BPMNSwimLane] ADD  CONSTRAINT [DF_BPMNSwimLane_SwimLaneX]  DEFAULT ((0)) FOR [SwimLaneX]
GO

ALTER TABLE [IMCloud].[BPMNSwimLane] ADD  CONSTRAINT [DF_BPMNSwimLane_SwimLaneY]  DEFAULT ((0)) FOR [SwimLaneY]
GO

ALTER TABLE [IMCloud].[BPMNSwimLane] ADD  CONSTRAINT [DF_BPMNSwimLane_SwimLaneWidth]  DEFAULT ((0)) FOR [SwimLaneWidth]
GO

ALTER TABLE [IMCloud].[BPMNSwimLane] ADD  CONSTRAINT [DF_BPMNSwimLane_SwimLaneHeight]  DEFAULT ((0)) FOR [SwimLaneHeight]
GO

ALTER TABLE [IMCloud].[BPMNSwimLane]  WITH CHECK ADD  CONSTRAINT [Process_SwimLane] FOREIGN KEY([ProcessId])
REFERENCES [IMCloud].[Process] ([ProcessID])
GO

ALTER TABLE [IMCloud].[BPMNSwimLane] CHECK CONSTRAINT [Process_SwimLane]
GO


