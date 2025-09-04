import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TaskReviewComponent } from "./task-review.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-review',
    component: TaskReviewComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TaskReviewComponent> = (args: TaskReviewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
