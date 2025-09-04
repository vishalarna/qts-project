import { Meta, moduleMetadata, Story } from '@storybook/angular';
import { CommonModule } from '@angular/common';
import { EmailEditorComponent } from './email-editor.component';
import { MatIconModule } from '@angular/material/icon';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

export default {
    title: 'Base Components/EmailEditor',
    component: EmailEditorComponent,
    decorators: [
      moduleMetadata({
        imports: [CommonModule, MatIconModule, CKEditorModule],
      }),
    ],
  } as Meta;

  const Template: Story<EmailEditorComponent> = (args: EmailEditorComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
