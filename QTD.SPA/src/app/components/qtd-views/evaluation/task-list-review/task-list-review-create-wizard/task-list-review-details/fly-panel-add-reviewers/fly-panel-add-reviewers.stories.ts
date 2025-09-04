import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelAddReviewersComponent } from "./fly-panel-add-reviewers.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-list-review-create-wizard/task-list-review-details/fly-panel-add-reviewers',
    component: FlyPanelAddReviewersComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelAddReviewersComponent> = (args: FlyPanelAddReviewersComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
