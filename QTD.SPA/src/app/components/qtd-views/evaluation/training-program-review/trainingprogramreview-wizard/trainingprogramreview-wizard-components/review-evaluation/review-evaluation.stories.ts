import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { ReviewEvaluationComponent } from "./review-evaluation.component";



export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/TrainingProgramReviewWizard/ReviewEvaluation',
    component: ReviewEvaluationComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<ReviewEvaluationComponent> = (args: ReviewEvaluationComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  