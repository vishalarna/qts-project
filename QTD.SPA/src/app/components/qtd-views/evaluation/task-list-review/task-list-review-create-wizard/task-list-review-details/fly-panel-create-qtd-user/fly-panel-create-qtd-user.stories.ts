import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelCreateQtdUserComponent } from './fly-panel-create-qtd-user.component';

export default {
    title: 'QTD Components/evaluation/task-list-review/task-list-review-create-wizard/task-list-review-details/fly-panel-create-qtd-user',
    component: FlyPanelCreateQtdUserComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelCreateQtdUserComponent> = (args: FlyPanelCreateQtdUserComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
