import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FilterListComponent } from "../filter-list/filter-list.component";
import { ReportsViewComponent } from "./reports-view.component";

export default {
    title: 'QTD Components/Report/ReportsView',
    component: ReportsViewComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent,FilterListComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<ReportsViewComponent> = (args: ReportsViewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
