import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { ConclusionAndActionItemsComponent } from "./conclusion-and-action-items.component";



export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/TrainingProgramReviewWizard/ConclusionAndActionItems',
    component: ConclusionAndActionItemsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<ConclusionAndActionItemsComponent> = (args: ConclusionAndActionItemsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  