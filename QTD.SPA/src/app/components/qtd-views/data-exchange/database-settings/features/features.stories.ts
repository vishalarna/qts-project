import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FeaturesComponent } from "./features.component";
export default {
    title: 'QTD Components/data-exchange/database-settings/features',
    component: FeaturesComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
  } as Meta;

  const Template: Story<FeaturesComponent> = (args: FeaturesComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});