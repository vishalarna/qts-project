import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelDifSurveyTasksComponent } from "./fly-panel-dif-survey-tasks.component";

export default {
    title: 'QTD Components/analysis/dif-surveys/dif-survey-create-wizard/dif-assign-task/fly-panel-dif-survey-tasks',
    component: FlyPanelDifSurveyTasksComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelDifSurveyTasksComponent> = (args: FlyPanelDifSurveyTasksComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
