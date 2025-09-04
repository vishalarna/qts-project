import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatIconModule } from "@angular/material/icon";
import { RouterTestingModule } from "@angular/router/testing";
import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { CertificationExpirationComponent } from './certification-expiration.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSelectModule } from "@angular/material/select";
import { TextareaComponent } from "src/app/components/base/textarea/textarea.component";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatChipsModule } from "@angular/material/chips";
import { NotificationsEnabledComponent } from "../../notifications-enabled/notifications-enabled.component";
import { EmailAddressEditorComponent } from "../../email-address-editor/email-address-editor.component";
import { ModelBindingsComponent } from "../../model-bindings/model-bindings.component";
import { EmailTemplateEditorComponent } from "../../email-template-editor/email-template-editor.component";
import * as data from '../../../../../../../assets/qtd-docs/clientSettings_notifications.json';
import { pascalToCamel } from "src/app/_Shared/Utils/PascalToCamel";
export default {
  title: 'QTD Components/EmailNotifications/CertificationExpiration',
  component: CertificationExpirationComponent,

  decorators: [
    moduleMetadata({
      declarations: [
        CertificationExpirationComponent,
        TextareaComponent,
        NotificationsEnabledComponent,
        EmailAddressEditorComponent,
        ModelBindingsComponent,
        EmailTemplateEditorComponent
      ],
      imports: [CommonModule, FormsModule, MatIconModule, RouterTestingModule, MatSlideToggleModule, MatExpansionModule, BrowserAnimationsModule,
        MatFormFieldModule, MatSelectModule,MatCheckboxModule,MatChipsModule,ReactiveFormsModule],
    }),
  ],
  argTypes: { notificationSettingsSaveSuccessEvent: { action: 'notificationSettingsSaveSuccessEvent' } },

} as Meta;

const Template: Story<CertificationExpirationComponent> = (args: CertificationExpirationComponent) => ({
  props: args,
});
const clientSettingSeedData = pascalToCamel(data);
const certificationExpireData = clientSettingSeedData[1].steps;

export const Default = Template.bind({});
Default.args = {

}

