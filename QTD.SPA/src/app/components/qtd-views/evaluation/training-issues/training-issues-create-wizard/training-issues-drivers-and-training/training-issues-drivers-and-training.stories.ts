import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingIssuesDriversAndTrainingComponent } from './training-issues-drivers-and-training.component';

export default {
    title: 'QTD Components/evaluation/training-issues/training-issues-create-wizard/training-issues-drivers-and-training',
    component: TrainingIssuesDriversAndTrainingComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingIssuesDriversAndTrainingComponent> = (args: TrainingIssuesDriversAndTrainingComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
