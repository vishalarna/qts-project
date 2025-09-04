import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import {  imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelOverviewFilterComponent } from "./fly-panel-overview-filter.component";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSelectModule } from "@angular/material/select";
import { LayoutModule } from "@angular/cdk/layout";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDatepickerModule } from "@angular/material/datepicker";


export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/FlyPanelOverview',
    component: FlyPanelOverviewFilterComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports:[...imports,MatFormFieldModule,
        MatSelectModule,
        LayoutModule,
        FormsModule,
        MatDatepickerModule,
        ReactiveFormsModule],
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelOverviewFilterComponent> = (args: FlyPanelOverviewFilterComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});