import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlypanelAddTrainingIssueActionItemComponent } from "./flypanel-add-training-issue-action-item.component";

export default {
    title: 'QTD Components/evaluation/training-issues/flypanel-add-training-issue-action-item',
    component: FlypanelAddTrainingIssueActionItemComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlypanelAddTrainingIssueActionItemComponent> = (args: FlypanelAddTrainingIssueActionItemComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
