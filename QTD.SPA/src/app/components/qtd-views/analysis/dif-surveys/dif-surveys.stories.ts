import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { DifSurveysComponent } from "./dif-surveys.component";

export default {
    title: 'QTD Components/analysis/dif-surveys',
    component: DifSurveysComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<DifSurveysComponent> = (args: DifSurveysComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
