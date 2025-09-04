import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import {  imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingIssuesReviewAndPublishComponent } from "./training-issues-review-and-publish.component";

export default {
    title: 'QTD Components/evaluation/training-issues/training-issues-create-wizard/training-issues-review-and-publish',
    component: TrainingIssuesReviewAndPublishComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingIssuesReviewAndPublishComponent> = (args: TrainingIssuesReviewAndPublishComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
