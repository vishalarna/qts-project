import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelAddMetaIlaComponent } from "./fly-panel-add-meta-ila.component";

export default {
    title: 'QTD Components/implementation/emp/idp/meta-idp-detail/fly-panel-add-meta-ila',
    component: FlyPanelAddMetaIlaComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelAddMetaIlaComponent> = (args: FlyPanelAddMetaIlaComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
