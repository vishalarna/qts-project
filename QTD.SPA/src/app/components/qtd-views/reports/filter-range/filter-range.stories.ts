import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { pascalToCamel } from "src/app/_Shared/Utils/PascalToCamel";
import { FilterRangeComponent } from "./filter-range.component";
import * as filterSkeletonData from '../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/report_skeleton.json';
import * as reportData from '../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/reports.json';

export default {
    title: 'QTD Components/Report/FilterRangeComponent',
    component: FilterRangeComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
    argTypes: { OnFilterUpdatedEvent: { action: '' } },
  } as Meta;
  
  const Template: Story<FilterRangeComponent> = (args: FilterRangeComponent) => ({
    props: args,
  });
  export const Default = Template.bind({ });
  Default.args = {
    reportSkeletonFilter: pascalToCamel(filterSkeletonData).filter(x => x.id == 1)[0].availableFilters[0],
  }