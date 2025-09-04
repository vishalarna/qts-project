import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TaskListReviewGenerateReportComponent } from "./task-list-review-generate-report.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-list-review-generate-report',
    component: TaskListReviewGenerateReportComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TaskListReviewGenerateReportComponent> = (args: TaskListReviewGenerateReportComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
