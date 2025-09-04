import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { NoTemplateComponent } from "./no-template.component";

export default {
    title: 'QTD Components/EmailNotifications/NoTemplate',
    component: NoTemplateComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
  } as Meta;

  const Template: Story<NoTemplateComponent> = (args: NoTemplateComponent) => ({
    props: args,
  });

  export const Default = Template.bind({});