import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlypanelTaskListReviewTasksComponent } from "./flypanel-task-list-review-tasks.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-list-review-create-wizard/task-list-review-tasks/flypanel-task-list-review-tasks',
    component: FlypanelTaskListReviewTasksComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlypanelTaskListReviewTasksComponent> = (args: FlypanelTaskListReviewTasksComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
