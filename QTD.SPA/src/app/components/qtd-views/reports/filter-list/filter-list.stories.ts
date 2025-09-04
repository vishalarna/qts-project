import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { pascalToCamel } from "src/app/_Shared/Utils/PascalToCamel";
import * as filterSkeletonData from '../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/report_skeleton.json';
import * as filterPositions from '../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/positions.json';
import { FilterListComponent } from "./filter-list.component";

export default {
    title: 'QTD Components/Report/FilterListComponent',
    component: FilterListComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],argTypes: { OnFilterUpdated: { action: 'OnFilterUpdated' } }
  } as Meta;
  
  const Template: Story<FilterListComponent> = (args: FilterListComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
  Default.args = {
    reportSkeletonFilter: pascalToCamel(filterSkeletonData).filter(x => x.id === 1)[0].availableFilters[1],
    positionData: pascalToCamel(filterPositions)
  }
