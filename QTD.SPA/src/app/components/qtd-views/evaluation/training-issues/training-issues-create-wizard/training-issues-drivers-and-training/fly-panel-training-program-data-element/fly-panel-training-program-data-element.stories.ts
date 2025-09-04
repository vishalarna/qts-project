import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelTrainingProgramDataElementComponent } from "./fly-panel-training-program-data-element.component";

export default {
    title: 'QTD Components/evaluation/training-issues/training-issues-create-wizard/training-issues-drivers-and-training/fly-panel-training-program-data-element',
    component: FlyPanelTrainingProgramDataElementComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelTrainingProgramDataElementComponent> = (args: FlyPanelTrainingProgramDataElementComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
