ALTER TABLE [IMCloud].[ProcessVariationStep]
ALTER COLUMN [ProcessVariationStepDescription] text;

ALTER TABLE [IMCloud].[ProcessVariationStep]
ALTER COLUMN [ProcessVariationStepDescriptionOriginal] text;


ALTER TABLE [IMCloud].[ProcessVariationGroup]
ALTER COLUMN [ProcessVariationGroupDescription] varchar(100);


ALTER TABLE [IMCloud].[ProcessVariationGroup]
ALTER COLUMN [ProcessVariationGroupDescriptionOriginal] varchar(250);