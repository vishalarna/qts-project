import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { ViewDifResultsComponent } from "./view-dif-results.component";


export default {
    title: 'QTD Components/analysis/dif-surveys/dif-survey-overview/view-dif-results',
    component: ViewDifResultsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<ViewDifResultsComponent> = (args: ViewDifResultsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
