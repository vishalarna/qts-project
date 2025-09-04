import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelFilterTaskListReviewComponent } from "./fly-panel-filter-task-list-review.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/fly-panel-filter-task-list-review',
    component: FlyPanelFilterTaskListReviewComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelFilterTaskListReviewComponent> = (args: FlyPanelFilterTaskListReviewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
