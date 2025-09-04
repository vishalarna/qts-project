import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlypanelStepOperationComponent } from "./flypanel-step-operation.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-review/flypanel-step-operation',
    component: FlypanelStepOperationComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlypanelStepOperationComponent> = (args: FlypanelStepOperationComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
