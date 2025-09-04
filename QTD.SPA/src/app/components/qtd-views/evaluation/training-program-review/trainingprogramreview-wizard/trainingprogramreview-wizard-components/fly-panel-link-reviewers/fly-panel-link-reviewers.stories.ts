import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelLinkReviewersComponent } from "./fly-panel-link-reviewers.component";

export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/TrainingProgramReviewWizard/CreateNewProgramReview/FlyPanelLinkReviewers',
    component: FlyPanelLinkReviewersComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelLinkReviewersComponent> = (args: FlyPanelLinkReviewersComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  