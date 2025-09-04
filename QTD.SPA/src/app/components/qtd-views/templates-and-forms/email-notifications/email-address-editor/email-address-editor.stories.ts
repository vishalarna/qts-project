import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { EmailAddressEditorComponent } from "./email-address-editor.component";


export default {
  title: 'QTD Components/EmailNotifications/EmailAddressEditor',
  component: EmailAddressEditorComponent,
  decorators: [
    moduleMetadata({
      declarations: declarations,
      imports: imports,
      providers: storybookProviders(),
    }),
  ],
  argTypes: { customRecipientsChangeEvent: { action: 'customRecipientsChangeEvent' } },
} as Meta;

const Template: Story<EmailAddressEditorComponent> = (args: EmailAddressEditorComponent) => ({
  props: args,
});

export const Default = Template.bind({});
Default.args = {
emailAddresses:[]
};



export const Write = Template.bind({});
Write.args = {
  mode: 'write',
  enabled: true,
  employees: false,
  managers: false,
  others: '',
  emailAddresses: []
};

export const Read = Template.bind({});
Read.args = {
  mode: 'read',
  enabled: false,
  employees: false,
  managers: false,
  others: '',
  emailAddresses: []
};

export const WithData = Template.bind({});
WithData.args = {
  mode: 'WithData',
  enabled: false,
  employees: true,
  managers: false,
  emailAddresses: [{
    'value': 'nik@gmail.com'
  },
  {
    'value': "qts@gmail.com"
  }
  ]
};
