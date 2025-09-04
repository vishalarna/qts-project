import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { EmployeePickerComponent } from "./employee-picker.component";
import { employeesTestData } from "src/app/_Services/QTD/Employees/testData";

export default {
    title: 'QTD Components/EmailNotifications/EmployeePicker',
    component: EmployeePickerComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
  } as Meta;

  const Template: Story<EmployeePickerComponent> = (args: EmployeePickerComponent) => ({
    props: args,
  });
  const employeeData = employeesTestData;
  export const Write = Template.bind({});
  Write.args = {
    mode: 'write',
    //Employees: employeeData
  };

  export const Read = Template.bind({});
  Read.args = {
    mode: 'read',
    //Employees: employeeData
  };
