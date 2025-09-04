import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelMetaIlaDataElementComponent } from "./fly-panel-meta-ila-data-element.component";

export default {
    title: 'QTD Components/evaluation/training-issues/training-issues-create-wizard/training-issues-drivers-and-training/fly-panel-meta-ila-data-element',
    component: FlyPanelMetaIlaDataElementComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelMetaIlaDataElementComponent> = (args: FlyPanelMetaIlaDataElementComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
