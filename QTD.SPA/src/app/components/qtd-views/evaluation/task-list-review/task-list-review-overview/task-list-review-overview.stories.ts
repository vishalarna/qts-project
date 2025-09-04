import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TaskListReviewOverviewComponent } from "./task-list-review-overview.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-list-review-overview',
    component: TaskListReviewOverviewComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TaskListReviewOverviewComponent> = (args: TaskListReviewOverviewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
