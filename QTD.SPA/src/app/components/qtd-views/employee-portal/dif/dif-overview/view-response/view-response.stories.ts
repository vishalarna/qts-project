import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { ViewResponseComponent } from "./view-response.component";


export default {
    title: 'QTD Components/employee-portal/dif/dif-overview/view-response',
    component: ViewResponseComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<ViewResponseComponent> = (args: ViewResponseComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
