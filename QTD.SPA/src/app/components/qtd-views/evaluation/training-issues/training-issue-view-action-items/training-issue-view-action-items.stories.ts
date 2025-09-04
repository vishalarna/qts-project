import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingIssueViewActionItemsComponent } from "./training-issue-view-action-items.component";

export default {
    title: 'QTD Components/evaluation/training-issues/training-issue-view-action-items',
    component: TrainingIssueViewActionItemsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingIssueViewActionItemsComponent> = (args: TrainingIssueViewActionItemsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
