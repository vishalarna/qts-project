import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingIssuesComponent } from "./training-issues.component";

export default {
    title: 'QTD Components/evaluation/training-issues',
    component: TrainingIssuesComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingIssuesComponent> = (args: TrainingIssuesComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
