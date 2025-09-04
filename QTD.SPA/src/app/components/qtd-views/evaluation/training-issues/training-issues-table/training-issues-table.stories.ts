import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import {  imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingIssuesTableComponent } from "./training-issues-table.component";

export default {
    title: 'QTD Components/evaluation/training-issues/training-issue-table',
    component: TrainingIssuesTableComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingIssuesTableComponent> = (args: TrainingIssuesTableComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
