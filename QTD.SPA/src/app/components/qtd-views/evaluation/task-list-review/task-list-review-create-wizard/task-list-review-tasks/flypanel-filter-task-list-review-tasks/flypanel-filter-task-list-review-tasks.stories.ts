import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlypanelFilterTaskListReviewTasksComponent } from "./flypanel-filter-task-list-review-tasks.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-list-review-create-wizard/task-list-review-tasks/flypanel-filter-task-list-review-tasks',
    component: FlypanelFilterTaskListReviewTasksComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlypanelFilterTaskListReviewTasksComponent> = (args: FlypanelFilterTaskListReviewTasksComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
