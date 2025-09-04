import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { PublicClassScheduleRequestComponent } from "./public-class-schedule-request.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";

export default {
    title: 'QTD Components/EmailNotifications/PublicClassScheduleRequest',
    component: PublicClassScheduleRequestComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
    argTypes: { notificationSettingsSaveSuccessEvent: { action: 'notificationSettingsSaveSuccessEvent' } },
  } as Meta;

  const Template: Story<PublicClassScheduleRequestComponent> = (args: PublicClassScheduleRequestComponent) => ({
    props: args,
  });