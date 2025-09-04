import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingDepartmentSignOffComponent } from "./training-department-sign-off.component";



export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/TrainingProgramReviewWizard/TrainingDepartmentSignOff',
    component: TrainingDepartmentSignOffComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingDepartmentSignOffComponent> = (args: TrainingDepartmentSignOffComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  