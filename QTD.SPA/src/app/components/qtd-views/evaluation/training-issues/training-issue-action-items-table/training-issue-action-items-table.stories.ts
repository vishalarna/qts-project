import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import {  imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingIssueActionItemsTableComponent } from "./training-issue-action-items-table.component";

export default {
    title: 'QTD Components/evaluation/training-issues/training-issue-action-items-table',
    component: TrainingIssueActionItemsTableComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingIssueActionItemsTableComponent> = (args: TrainingIssueActionItemsTableComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
