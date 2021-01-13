ALTER TABLE [IMCloud].[ProcessVariationGroup] ADD BPMNSwimLaneId int  NULL;

ALTER TABLE [IMCloud].[ProcessVariationGroup]  WITH CHECK ADD  CONSTRAINT [BPMNSwimLane_ProcessVariationGroup] FOREIGN KEY([BPMNSwimLaneId])
REFERENCES [IMCloud].[BPMNSwimLane] ([BPMNSwimLaneId]);