USE TRITON_8000PERFORMANCE

declare @collectionRoundId as uniqueidentifier
SET @collectionRoundId = '68A60FEE-F693-423F-8BA8-DB2F374E2A70'


	SELECT 
    [Extent2].[Id] AS [Id], 
    [Extent2].[CollectionRoundId] AS [CollectionRoundId], 
    [Extent2].[Type] AS [Type], 
    [Extent2].[Name] AS [Name], 
    [Extent2].[Stratum] AS [Stratum], 
    [Extent2].[DesignWeight] AS [DesignWeight], 
    [Extent2].[CreatedBy] AS [CreatedBy], 
    [Extent2].[CreatedDate] AS [CreatedDate], 
    [Extent2].[UpdatedBy] AS [UpdatedBy], 
    [Extent2].[UpdatedDate] AS [UpdatedDate], 
    [Extent2].[LoginGroupId] AS [LoginGroupId], 
    [Extent2].[BKSUsername] AS [BKSUsername], 
    [Extent2].[BKSPassword] AS [BKSPassword], 
    [Extent2].[SCBId] AS [SCBId], 
    [Extent2].[Selektor] AS [Selektor], 
    [Extent2].[OrganizationalChangeNote] AS [OrganizationalChangeNote], 
    [Extent2].[StatusIndicator] AS [StatusIndicator], 
    [Extent2].[StatusAdditionalIndicator] AS [StatusAdditionalIndicator], 
    [Extent2].[StatusCreatedDate] AS [StatusCreatedDate], 
    [Extent2].[StatusCreatedBy] AS [StatusCreatedBy]
    FROM  [Triton].[vwAnswerPreviousSearch] AS [Extent1]
    INNER JOIN [Triton].[vwCollectionUnitWithCurrentStatus] AS [Extent2] ON [Extent1].[fkCollectionUnitId] = [Extent2].[Id]
    WHERE ([Extent1].[Type] IN (N'Lon', N'NegativtHeltal', N'VarAlice')) AND ([Extent2].[CollectionRoundId] = @collectionRoundId)
//comment
