import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlypanelSubdutyOperationComponent } from "./flypanel-subduty-operation.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-review/flypanel-subduty-operation',
    component: FlypanelSubdutyOperationComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlypanelSubdutyOperationComponent> = (args: FlypanelSubdutyOperationComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
