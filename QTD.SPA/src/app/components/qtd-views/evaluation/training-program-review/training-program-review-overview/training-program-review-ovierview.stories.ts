import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { TrainingProgramReviewOverviewComponent } from "./training-program-review-overview.component";
import { FlyPanelOverviewFilterComponent } from "../fly-panel-overview-filter/fly-panel-overview-filter.component";
import { LayoutModule } from "@angular/cdk/layout";
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { FlyPanelOverviewFilterModule } from "../fly-panel-overview-filter/fly-panel-overview-filter.module";


export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/Overview',
    component: TrainingProgramReviewOverviewComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: [...imports,MatPaginatorModule],
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<TrainingProgramReviewOverviewComponent> = (args: TrainingProgramReviewOverviewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});