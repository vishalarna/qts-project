import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { CbtManagerComponent } from "./cbt-manager.component";

export default {
    title: 'QTD Components/shared/CbtManager',
    component: CbtManagerComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
    argTypes: {
      OnAttachCourseBegin: { action: 'onAttachCourseAsync' },
      OnAttachCourseSuccess: { action: 'onAttachCourseAsync' },
      OnAttachCourseError: { action: 'onAttachCourseAsync' },
      OnDisconnectCourseBegin: { action: 'onDisconnectCourseAsync' },
      OnDisconnectCourseSuccess: { action: 'onDisconnectCourseAsync' },
      OnDisconnectCourseError: { action: 'onDisconnectCourseAsync' }
    }  
  } as Meta;
  const Template: Story<CbtManagerComponent> = (args: CbtManagerComponent) => ({
    props: args,
  });
  
  export const Default = Template.bind({});

