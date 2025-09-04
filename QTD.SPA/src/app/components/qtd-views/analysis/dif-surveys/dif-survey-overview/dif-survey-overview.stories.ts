import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { DifSurveyOverviewComponent } from "./dif-survey-overview.component";

export default {
    title: 'QTD Components/analysis/dif-surveys/dif-survey-overview',
    component: DifSurveyOverviewComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<DifSurveyOverviewComponent> = (args: DifSurveyOverviewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
