import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FilterSingleComponent } from "./filter-single.component";

export default {
    title: 'QTD Components/Report/FilterSingleComponent',
    component: FilterSingleComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
  } as Meta;
  
  const Template: Story<FilterSingleComponent> = (args: FilterSingleComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});