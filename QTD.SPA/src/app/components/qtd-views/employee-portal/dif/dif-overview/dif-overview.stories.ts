import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { DifOverviewComponent } from "./dif-overview.component";

export default {
    title: 'QTD Components/employee-portal/dif/dif-overview',
    component: DifOverviewComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<DifOverviewComponent> = (args: DifOverviewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
