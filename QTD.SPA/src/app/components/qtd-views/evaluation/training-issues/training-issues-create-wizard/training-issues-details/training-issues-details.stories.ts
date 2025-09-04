import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingIssuesDetailsComponent } from "./training-issues-details.component";

export default {
    title: 'QTD Components/evaluation/training-issues/training-issues-create-wizard/training-issues-details',
    component: TrainingIssuesDetailsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingIssuesDetailsComponent> = (args: TrainingIssuesDetailsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
