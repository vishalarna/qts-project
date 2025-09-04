import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelDocumentStorageComponent } from "./fly-panel-document-storage.component";
import { DocumentDisplayMode } from "src/app/_DtoModels/Document/DocumentDisplayMode";
import { DocumentViewModel } from "src/app/_DtoModels/Document/DocumentViewModel";



export default {
    title: 'QTD Components/DocumentStorage/FlyPanelDocumentStorage',
    component: FlyPanelDocumentStorageComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelDocumentStorageComponent> = (args: FlyPanelDocumentStorageComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
  export const Edit = Template.bind({});
  export const View = Template.bind({});

  Default.args ={
    documentDisplayMode: DocumentDisplayMode.Add
  }
  View.args = {
    documentDisplayMode: DocumentDisplayMode.View,
    document:{
      id: '1',
      fileName: 'd.pdf',
      comments: 'test comments',
      dateAdded: 'd',
      documentTypeId: '2',
      documentTypeName: 'employees',
      linkedDataId: '3',
      linkedDataName: 'dd',
    } as DocumentViewModel
  }
  Edit.args={
    documentDisplayMode:DocumentDisplayMode.Edit,
    document:{
      id: '1',
      fileName: 'd.pdf',
      comments: 'test comments',
      dateAdded: 'd',
      documentTypeId: '2',
      documentTypeName: 'employees',
      linkedDataId: '3',
      linkedDataName: 'dd',
    } as DocumentViewModel
  }