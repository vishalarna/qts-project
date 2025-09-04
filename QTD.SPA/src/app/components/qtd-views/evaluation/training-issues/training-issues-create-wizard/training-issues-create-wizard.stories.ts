import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingIssuesCreateWizardComponent } from "./training-issues-create-wizard.component";

export default {
    title: 'QTD Components/evaluation/training-issues/training-issues-create-wizard',
    component: TrainingIssuesCreateWizardComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingIssuesCreateWizardComponent> = (args: TrainingIssuesCreateWizardComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
