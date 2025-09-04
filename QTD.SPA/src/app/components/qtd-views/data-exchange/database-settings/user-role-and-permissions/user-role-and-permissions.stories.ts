import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { UserRoleAndPermissionsComponent } from "./user-role-and-permissions.component";
export default {
    title: 'QTD Components/DataExchange/User role and permission',
    component: UserRoleAndPermissionsComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
  } as Meta;

  const Template: Story<UserRoleAndPermissionsComponent> = (args: UserRoleAndPermissionsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});