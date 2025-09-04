import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelCreatePersonComponent } from "./fly-panel-create-person.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-list-review-create-wizard/task-list-review-details/fly-panel-create-person',
    component: FlyPanelCreatePersonComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelCreatePersonComponent> = (args: FlyPanelCreatePersonComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
