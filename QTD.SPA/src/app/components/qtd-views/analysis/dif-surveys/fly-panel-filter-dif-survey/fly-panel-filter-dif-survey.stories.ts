import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelFilterDifSurveyComponent } from "./fly-panel-filter-dif-survey.component";

export default {
    title: 'QTD Components/analysis/dif-surveys/fly-panel-filter-dif-survey',
    component: FlyPanelFilterDifSurveyComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelFilterDifSurveyComponent> = (args: FlyPanelFilterDifSurveyComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
