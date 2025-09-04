import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlypanelFilterTrainingIssueActionItemsComponent } from "./flypanel-filter-training-issue-action-items.component";

export default {
    title: 'QTD Components/evaluation/training-issues/training-issue-view-action-items/flypanel-filter-training-issue-action-items',
    component: FlypanelFilterTrainingIssueActionItemsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlypanelFilterTrainingIssueActionItemsComponent> = (args: FlypanelFilterTrainingIssueActionItemsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
