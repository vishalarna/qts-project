import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { PublicRequestsComponent } from "./public-requests.component";

export default {
    title: 'QTD Components/home/public-requests',
    component: PublicRequestsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<PublicRequestsComponent> = (args: PublicRequestsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
