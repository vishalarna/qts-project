import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { ViewEnrollmentComponent } from "./view-enrollment.component";

export default {
    title: 'QTD Components/analysis/dif-surveys/dif-survey-overview/view-enrollment',
    component: ViewEnrollmentComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<ViewEnrollmentComponent> = (args: ViewEnrollmentComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
