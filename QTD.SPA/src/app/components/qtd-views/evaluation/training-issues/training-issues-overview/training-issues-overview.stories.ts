import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingIssuesOverviewComponent } from "./training-issues-overview.component";

export default {
    title: 'QTD Components/evaluation/training-issues/training-issues-overview',
    component: TrainingIssuesOverviewComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingIssuesOverviewComponent> = (args: TrainingIssuesOverviewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
