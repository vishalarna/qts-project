import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelTrainingIssuesPendingActionItemComponent } from "./fly-panel-training-issues-pending-action-item.component";

export default {
    title: 'QTD Components/evaluation/training-issues/fly-panel-training-issues-pending-action-item',
    component: FlyPanelTrainingIssuesPendingActionItemComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelTrainingIssuesPendingActionItemComponent> = (args: FlyPanelTrainingIssuesPendingActionItemComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
