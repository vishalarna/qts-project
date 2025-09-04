import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import {  imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingIssuesActionItemsComponent } from "./training-issues-action-items.component";

export default {
    title: 'QTD Components/evaluation/training-issues/training-issues-create-wizard/training-issues-action-items',
    component: TrainingIssuesActionItemsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingIssuesActionItemsComponent> = (args: TrainingIssuesActionItemsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
