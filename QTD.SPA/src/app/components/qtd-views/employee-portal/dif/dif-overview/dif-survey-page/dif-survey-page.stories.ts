import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { imports, storybookProviders } from "src/app/app.module.meta";
import { DifSurveyPageComponent } from "./dif-survey-page.component";
 
export default {
    title: 'QTD Components/employee-portal/dif/dif-overview/dif-survey-page',
    component: DifSurveyPageComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;
 
  const Template: Story<DifSurveyPageComponent> = (args: DifSurveyPageComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});