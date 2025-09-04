import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { EnrollMetaILAStudentsComponent } from "./enroll-meta-ila-students.component";



export default {
    title: 'QTD Components/ProviderAndILA/CreateMetaILAWizard/EnrollMetaILAStudents',
    component: EnrollMetaILAStudentsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<EnrollMetaILAStudentsComponent> = (args: EnrollMetaILAStudentsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  