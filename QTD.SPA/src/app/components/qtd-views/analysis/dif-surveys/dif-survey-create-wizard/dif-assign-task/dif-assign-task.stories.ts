import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { DifAssignTaskComponent } from "./dif-assign-task.component";

export default {
    title: 'QTD Components/analysis/dif-surveys/dif-survey-create-wizard/dif-assign-task',
    component: DifAssignTaskComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<DifAssignTaskComponent> = (args: DifAssignTaskComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
