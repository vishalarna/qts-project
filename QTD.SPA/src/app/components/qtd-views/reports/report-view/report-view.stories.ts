import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { ReportViewComponent } from "./report-view.component";

export default {
    title: 'QTD Components/Report/ReportView',
    component: ReportViewComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<ReportViewComponent> = (args: ReportViewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
