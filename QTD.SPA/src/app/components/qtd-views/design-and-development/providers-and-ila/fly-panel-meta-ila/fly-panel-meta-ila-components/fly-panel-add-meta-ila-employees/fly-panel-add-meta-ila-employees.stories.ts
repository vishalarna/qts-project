import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelAddMetaILAEmployeesComponent } from "./fly-panel-add-meta-ila-employees.component";

export default {
    title: 'QTD Components/ProviderAndILA/CreateMetaILAWizard/EnrollMetaILAStudents/FlyPanelAddMetaILAEmployees',
    component: FlyPanelAddMetaILAEmployeesComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelAddMetaILAEmployeesComponent> = (args: FlyPanelAddMetaILAEmployeesComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  