import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations,imports, storybookProviders } from "src/app/app.module.meta";
import { SupportingDocumentsAndIssuesComponent } from "./supporting-documents-and-issues.component";
import { DocumentTableModule } from "src/app/components/qtd-views/document-storage/document-table/document-table.module";
import { FlyPanelLinkTrainingIssuesModule } from "../fly-panel-link-training-issues/fly-panel-link-training-issues.module";
import { MatIconModule } from "@angular/material/icon";



export default {
    title: 'QTD Components/Evaluation/TrainingProgramReview/TrainingProgramReviewWizard/SupportingDocumentsAndIssues',
    component: SupportingDocumentsAndIssuesComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports:[...imports,DocumentTableModule,FlyPanelLinkTrainingIssuesModule,MatIconModule],
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<SupportingDocumentsAndIssuesComponent> = (args: SupportingDocumentsAndIssuesComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  