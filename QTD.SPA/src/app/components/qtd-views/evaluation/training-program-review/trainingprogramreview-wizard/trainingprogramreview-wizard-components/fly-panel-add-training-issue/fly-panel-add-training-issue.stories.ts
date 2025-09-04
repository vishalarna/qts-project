import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelAddTrainingIssueComponent } from "./fly-panel-add-training-issue.component"

export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/TrainingProgramReviewWizard/SupportingDocumentsAndIssues/FlyPanelAddTrainingIssue',
    component: FlyPanelAddTrainingIssueComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelAddTrainingIssueComponent> = (args: FlyPanelAddTrainingIssueComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  