import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelLinkTrainingIssuesComponent } from "./fly-panel-link-training-issues.component";

export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/TrainingProgramReviewWizard/SupportingDocumentsAndIssues/FlyPanelLinkTrainingIssues',
    component: FlyPanelLinkTrainingIssuesComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelLinkTrainingIssuesComponent> = (args: FlyPanelLinkTrainingIssuesComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  