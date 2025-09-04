import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { PurposeOfReviewComponent } from "./purpose-of-review.component";



export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/TrainingProgramReviewWizard/PurposeOfReview',
    component: PurposeOfReviewComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<PurposeOfReviewComponent> = (args: PurposeOfReviewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  