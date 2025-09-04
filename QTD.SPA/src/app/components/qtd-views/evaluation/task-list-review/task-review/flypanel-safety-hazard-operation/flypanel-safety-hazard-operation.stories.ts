import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlypanelSafetyHazardOperationComponent } from "./flypanel-safety-hazard-operation.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-review/flypanel-safety-hazard-operation',
    component: FlypanelSafetyHazardOperationComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlypanelSafetyHazardOperationComponent> = (args: FlypanelSafetyHazardOperationComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
