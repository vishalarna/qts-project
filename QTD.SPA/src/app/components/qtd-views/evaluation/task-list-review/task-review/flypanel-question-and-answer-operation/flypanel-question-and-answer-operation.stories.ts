import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlypanelQuestionAndAnswerOperationComponent } from "./flypanel-question-and-answer-operation.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-review/flypanel-question-and-answer-operation',
    component: FlypanelQuestionAndAnswerOperationComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlypanelQuestionAndAnswerOperationComponent> = (args: FlypanelQuestionAndAnswerOperationComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
