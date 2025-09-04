import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlypanelEnablingObjectiveOperationComponent } from "./flypanel-enabling-objective-operation.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-review/flypanel-enabling-objective-operation',
    component: FlypanelEnablingObjectiveOperationComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlypanelEnablingObjectiveOperationComponent> = (args: FlypanelEnablingObjectiveOperationComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
