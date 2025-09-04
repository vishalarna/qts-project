import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { DocumentStorageViewComponent } from "./document-storage-view.component";
import { DocumentTableComponent } from "../document-table/document-table.component";


export default {
    title: 'QTD Components/DocumentStorage/View',
    component: DocumentStorageViewComponent,
    decorators: [
      moduleMetadata({
        declarations: [DocumentTableComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<DocumentStorageViewComponent> = (args: DocumentStorageViewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});