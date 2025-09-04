import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { ImportDifSurveyComponent } from "./import-dif-survey.component";

export default {
    title: 'QTD Components/analysis/dif-surveys/dif-survey-overview/import-dif-survey',
    component: ImportDifSurveyComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<ImportDifSurveyComponent> = (args: ImportDifSurveyComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
