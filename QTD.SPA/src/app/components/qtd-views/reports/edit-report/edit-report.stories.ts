import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FilterListComponent } from "../filter-list/filter-list.component";
import { FilterRangeComponent } from "../filter-range/filter-range.component";
import { FilterSingleComponent } from "../filter-single/filter-single.component";
import { ReportViewComponent } from "../report-view/report-view.component";
import { EditReportComponent } from './edit-report.component';

export default {
    title: 'QTD Components/Report/EditReport',
    component: EditReportComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent,FilterListComponent,FilterRangeComponent, FilterSingleComponent,ReportViewComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<EditReportComponent> = (args: EditReportComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});