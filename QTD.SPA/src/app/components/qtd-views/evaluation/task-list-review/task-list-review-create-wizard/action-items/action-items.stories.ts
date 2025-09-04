import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { ActionItemsComponent } from "./action-items.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-list-review-create-wizard/action-items',
    component: ActionItemsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<ActionItemsComponent> = (args: ActionItemsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
