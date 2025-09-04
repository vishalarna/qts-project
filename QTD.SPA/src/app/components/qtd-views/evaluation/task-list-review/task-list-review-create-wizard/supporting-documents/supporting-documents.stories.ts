import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { SupportingDocumentsComponent } from "./supporting-documents.component";

export default {
    title: 'QTD Components/evaluation/task-list-review/task-list-review-create-wizard/supporting-documents',
    component: SupportingDocumentsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<SupportingDocumentsComponent> = (args: SupportingDocumentsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
