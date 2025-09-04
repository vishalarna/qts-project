import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlypanelToolOperationComponent } from "./flypanel-tool-operation.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-review/flypanel-tool-operation',
    component: FlypanelToolOperationComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlypanelToolOperationComponent> = (args: FlypanelToolOperationComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
