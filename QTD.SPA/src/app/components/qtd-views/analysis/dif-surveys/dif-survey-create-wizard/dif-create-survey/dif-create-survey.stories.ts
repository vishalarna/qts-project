import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { DifCreateSurveyComponent } from "./dif-create-survey.component";


export default {
    title: 'QTD Components/analysis/dif-surveys/dif-survey-create-wizard/dif-create-survey',
    component: DifCreateSurveyComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<DifCreateSurveyComponent> = (args: DifCreateSurveyComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
