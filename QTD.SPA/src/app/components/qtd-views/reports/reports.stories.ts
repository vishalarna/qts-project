import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { ReportsViewComponent } from "./reports-view/reports-view.component";
import { ReportsComponent } from "./reports.component";

export default {
    title: 'QTD Components/Report/reports',
    component: ReportsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent, ReportsViewComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<ReportsComponent> = (args: ReportsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});