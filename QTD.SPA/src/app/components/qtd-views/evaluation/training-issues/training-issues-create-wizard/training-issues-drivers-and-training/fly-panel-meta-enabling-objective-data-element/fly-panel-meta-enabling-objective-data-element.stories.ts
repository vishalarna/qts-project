import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelMetaEnablingObjectiveDataElementComponent } from "./fly-panel-meta-enabling-objective-data-element.component";

export default {
    title: 'QTD Components/evaluation/training-issues/training-issues-create-wizard/training-issues-drivers-and-training/fly-panel-meta-enabling-objective-data-element',
    component: FlyPanelMetaEnablingObjectiveDataElementComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelMetaEnablingObjectiveDataElementComponent> = (args: FlyPanelMetaEnablingObjectiveDataElementComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
