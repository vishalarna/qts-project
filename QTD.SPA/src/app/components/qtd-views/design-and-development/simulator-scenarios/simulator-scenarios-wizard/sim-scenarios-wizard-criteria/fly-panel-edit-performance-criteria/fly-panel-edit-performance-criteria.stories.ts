import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelEditPerformanceCriteriaComponent } from "./fly-panel-edit-performance-criteria.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/simulator-scenarios-wizard-criteria/fly-panel-edit-performance-criteria',
    component: FlyPanelEditPerformanceCriteriaComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelEditPerformanceCriteriaComponent> = (args: FlyPanelEditPerformanceCriteriaComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
