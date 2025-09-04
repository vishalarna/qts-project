import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { pascalToCamel } from "src/app/_Shared/Utils/PascalToCamel";
import * as filterSkeletonData from '../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/report_skeleton.json';
import * as filterPositions from '../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/positions.json';
import { FilterParentComponent } from "./filter-parent.component";

export default {
    title: 'QTD Components/Report/FilterParentComponent',
    component: FilterParentComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;
  
  const Template: Story<FilterParentComponent> = (args: FilterParentComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
  Default.args = {
    reportSkeletonFilter: pascalToCamel(filterSkeletonData).filter(x => x.id === 1)[0].availableFilters[1],
    positionData: pascalToCamel(filterPositions)
  }
