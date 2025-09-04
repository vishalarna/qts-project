import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelDifSurveyEmployeesComponent } from "./fly-panel-dif-survey-employees.component";

export default {
    title: 'QTD Components/analysis/dif-surveys/dif-survey-create-wizard/dif-assign-employees/fly-panel-dif-survey-employees',
    component: FlyPanelDifSurveyEmployeesComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelDifSurveyEmployeesComponent> = (args: FlyPanelDifSurveyEmployeesComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
