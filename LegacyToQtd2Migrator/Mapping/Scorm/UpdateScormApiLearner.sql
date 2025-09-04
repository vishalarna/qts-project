
ALTER TABLE [dbo].[ScormApiRegToLearner]
NOCHECK CONSTRAINT  FK_ScormRegToLearn_learner

BEGIN TRAN
Update [MigrationTest].[dbo].[ScormApiLearner]
set Api_learner_id = @newLearnerId where api_learner_id = @oldLearnerId

update [dbo].[ScormApiRegToLearner] set api_learner_id = 1244 where api_learner_id = 15

COMMIT TRAN
  
ALTER TABLE [ScormApiRegToLearner] 
CHECK CONSTRAINT FK_ScormRegToLearn_learner