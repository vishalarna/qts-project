import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { ReviewDesignAndDevelopmentComponent } from "./review-design-and-development.component";



export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/TrainingProgramReviewWizard/ReviewDesignAndDevelopment',
    component: ReviewDesignAndDevelopmentComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<ReviewDesignAndDevelopmentComponent> = (args: ReviewDesignAndDevelopmentComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  