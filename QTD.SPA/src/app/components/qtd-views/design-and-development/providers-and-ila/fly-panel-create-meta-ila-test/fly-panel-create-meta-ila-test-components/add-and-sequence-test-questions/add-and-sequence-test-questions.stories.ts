import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { AddAndSequenceTestQuestionsComponent } from "./add-and-sequence-test-questions.component";
import { MatTableModule } from "@angular/material/table";
import { DragDropModule } from "@angular/cdk/drag-drop";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatIconModule } from "@angular/material/icon";



export default {
    title: 'QTD Components/ProviderAndILA/CreateMetaILAWizard/CreateMetaILATestWizard/AddAndSequenceTestQuestions',
    component: AddAndSequenceTestQuestionsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: [...imports, MatTableModule,DragDropModule,MatCheckboxModule,MatIconModule],
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<AddAndSequenceTestQuestionsComponent> = (args: AddAndSequenceTestQuestionsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  