import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { ConclusionAndTrainingComponent } from "./conclusion-and-training.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-list-review-create-wizard/conclusion-and-training',
    component: ConclusionAndTrainingComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<ConclusionAndTrainingComponent> = (args: ConclusionAndTrainingComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
