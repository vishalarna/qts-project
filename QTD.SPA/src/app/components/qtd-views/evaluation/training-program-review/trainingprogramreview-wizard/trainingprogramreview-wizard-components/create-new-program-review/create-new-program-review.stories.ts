import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { CreateNewProgramReviewComponent } from "./create-new-program-review.component";



export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/TrainingProgramReviewWizard/CreateNewProgramReview',
    component: CreateNewProgramReviewComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<CreateNewProgramReviewComponent> = (args: CreateNewProgramReviewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  