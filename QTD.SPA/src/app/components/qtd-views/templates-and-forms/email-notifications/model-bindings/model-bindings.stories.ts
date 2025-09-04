import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from 'src/app/app.module.meta';

import * as data from '../../../../../../assets/qtd-docs/clientSettings_notifications.json'
import { pascalToCamel } from 'src/app/_Shared/Utils/PascalToCamel';
import { ModelBindingsComponent } from './model-bindings.component';

export default {
    title: 'QTD Components/EmailNotifications/ModelBindings',
    component: ModelBindingsComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ] ,
    argTypes: { modelItemButtonClicked: { action: 'modelItemButtonClicked' } },
  } as Meta;

  const Template: Story<ModelBindingsComponent> = (args: ModelBindingsComponent) => ({
    props: args,
  });
  const clientSettingSeedData = pascalToCamel(data);
  const ModelBindingValues = clientSettingSeedData[0].steps;
  export const Read = Template.bind({});
  Read.args = {
    modelItems:ModelBindingValues[0].model,
    mode: "read",
  };

  export const Write = Template.bind({});
  Write.args = {
    modelItems:ModelBindingValues[0].model,
    mode: "write",
  };
