import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { DifSurveyCreateWizardComponent } from "./dif-survey-create-wizard.component";

export default {
    title: 'QTD Components/analysis/dif-surveys/dif-survey-create-wizard',
    component: DifSurveyCreateWizardComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<DifSurveyCreateWizardComponent> = (args: DifSurveyCreateWizardComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
