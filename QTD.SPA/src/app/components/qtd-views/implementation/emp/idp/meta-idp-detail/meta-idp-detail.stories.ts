import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { MetaIdpDetailComponent } from "./meta-idp-detail.component";

export default {
    title: 'QTD Components/implementation/emp/idp/meta-idp-detail',
    component: MetaIdpDetailComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<MetaIdpDetailComponent> = (args: MetaIdpDetailComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
