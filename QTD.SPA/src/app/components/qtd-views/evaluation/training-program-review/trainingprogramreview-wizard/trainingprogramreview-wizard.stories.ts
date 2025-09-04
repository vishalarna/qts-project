import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingProgramReviewWizardComponent } from "./trainingprogramreview-wizard.component";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatStepperModule } from "@angular/material/stepper";
import { CreateNewProgramReviewModule } from "./trainingprogramreview-wizard-components/create-new-program-review/create-new-program-review.module";
import { PurposeOfReviewModule } from "./trainingprogramreview-wizard-components/purpose-of-review/purpose-of-review.module";
import { ReviewDesignAndDevelopmentModule } from "./trainingprogramreview-wizard-components/review-design-and-development/review-design-and-development.module";
import { ReviewEvaluationModule } from "./trainingprogramreview-wizard-components/review-evaluation/review-evaluation.module";
import { SupportingDocumentsAndIssuesModule } from "./trainingprogramreview-wizard-components/supporting-documents-and-issues/supporting-documents-and-issues.module";
import { ConclusionAndActionItemsModule } from "./trainingprogramreview-wizard-components/conclusion-and-action-items/conclusion-and-action-items.module";
import { TrainingDepartmentSignOffModule } from "./trainingprogramreview-wizard-components/training-department-sign-off/training-department-sign-off.module";



export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/TrainingProgramReviewWizard',
    component: TrainingProgramReviewWizardComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: [...imports,MatToolbarModule,MatStepperModule,CreateNewProgramReviewModule,
          PurposeOfReviewModule,ReviewDesignAndDevelopmentModule,ReviewEvaluationModule,
          SupportingDocumentsAndIssuesModule,ConclusionAndActionItemsModule,TrainingDepartmentSignOffModule],
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingProgramReviewWizardComponent> = (args: TrainingProgramReviewWizardComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  