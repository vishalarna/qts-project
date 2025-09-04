import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { DifAssignEmployeesComponent } from "./dif-assign-employees.component";

export default {
    title: 'QTD Components/analysis/dif-surveys/dif-survey-create-wizard/dif-assign-employees',
    component: DifAssignEmployeesComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<DifAssignEmployeesComponent> = (args: DifAssignEmployeesComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
