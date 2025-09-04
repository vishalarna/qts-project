import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { PublicPortalDashboardComponent } from "./public-portal-dashboard.component";

export default {
    title: 'QTD Components/public-portal/public-portal-dashboard',
    component: PublicPortalDashboardComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<PublicPortalDashboardComponent> = (args: PublicPortalDashboardComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
