import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TopNavBarComponent } from "./top-nav-bar.component";


export default {
  title: 'QTD Components/EmailNotifications/TopNavBarComponent',
  component: TopNavBarComponent,
  decorators: [
    moduleMetadata({
      declarations: declarations,
      imports: imports,
      providers: storybookProviders(),
    }),
  ],
} as Meta;

const Template: Story<TopNavBarComponent> = (args: TopNavBarComponent) => ({
  props: args,
});


export const Write = Template.bind({});
Write.args = {
  mode: 'write',
};

export const Read = Template.bind({});
Read.args = {
  mode: 'read',
};