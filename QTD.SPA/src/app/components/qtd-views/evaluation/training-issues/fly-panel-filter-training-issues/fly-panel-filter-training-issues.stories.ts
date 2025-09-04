import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelFilterTrainingIssuesComponent } from "./fly-panel-filter-training-issues.component";

export default {
    title: 'QTD Components/evaluation/training-issues/fly-panel-filter-training-issues',
    component: FlyPanelFilterTrainingIssuesComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelFilterTrainingIssuesComponent> = (args: FlyPanelFilterTrainingIssuesComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
