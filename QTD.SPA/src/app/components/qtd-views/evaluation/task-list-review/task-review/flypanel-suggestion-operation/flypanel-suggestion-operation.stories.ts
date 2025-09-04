import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlypanelSuggestionOperationComponent } from "./flypanel-suggestion-operation.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-review/flypanel-suggestion-operation',
    component: FlypanelSuggestionOperationComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlypanelSuggestionOperationComponent> = (args: FlypanelSuggestionOperationComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
