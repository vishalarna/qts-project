import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { DocumentTableComponent } from "./document-table.component";
import { pascalToCamel } from "src/app/_Shared/Utils/PascalToCamel";
import * as documentsList from '../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/documentStorage.json';
import { FlyPanelDocumentStorageComponent } from "../fly-panel-document-storage/fly-panel-document-storage.component";

export default {
    title: 'QTD Components/DocumentStorage/DocumentTable',
    component: DocumentTableComponent,
    decorators: [
      moduleMetadata({
        declarations: [FlyPanelDocumentStorageComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<DocumentTableComponent> = (args: DocumentTableComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
  Default.args = {
    documents: pascalToCamel(documentsList)
  }