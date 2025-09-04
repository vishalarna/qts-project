import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlypanelProcedureOperationComponent } from "./flypanel-procedure-operation.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-review/flypanel-procedure-operation',
    component: FlypanelProcedureOperationComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlypanelProcedureOperationComponent> = (args: FlypanelProcedureOperationComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
