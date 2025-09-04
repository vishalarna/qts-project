import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { DifReviewAndPublishComponent } from "./dif-review-and-publish.component";

export default {
    title: 'QTD Components/analysis/dif-surveys/dif-survey-create-wizard/dif-review-and-publish',
    component: DifReviewAndPublishComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<DifReviewAndPublishComponent> = (args: DifReviewAndPublishComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
