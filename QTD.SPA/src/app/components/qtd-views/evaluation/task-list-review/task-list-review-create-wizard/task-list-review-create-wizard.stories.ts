import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TaskListReviewCreateWizardComponent } from "./task-list-review-create-wizard.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-list-review-create-wizard',
    component: TaskListReviewCreateWizardComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TaskListReviewCreateWizardComponent> = (args: TaskListReviewCreateWizardComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
