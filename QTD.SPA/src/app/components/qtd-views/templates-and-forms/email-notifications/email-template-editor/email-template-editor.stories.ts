import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { EmailTemplateEditorComponent } from "./email-template-editor.component";

export default {
    title: 'QTD Components/EmailNotifications/EmailTemplateEditor',
    component: EmailTemplateEditorComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
    argTypes: {onEmailTemplateContentChange: {action: 'onEmailTemplateContentChange'}},
  } as Meta;

  const Template: Story<EmailTemplateEditorComponent> = (args: EmailTemplateEditorComponent) => ({
    props: args,
  });

  export const Read = Template.bind({});
  Read.args = {
    emailData: "This is Read Template",
    mode: "read"
  }

  export const Write = Template.bind({});
  Write.args = {
    emailData: "This is Write Template",
    mode: "write"
  }
