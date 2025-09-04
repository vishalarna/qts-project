import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { LabelReplacementPipe } from "src/app/_Pipes/label-replacement.pipe";
import { pascalToCamel } from "src/app/_Shared/Utils/PascalToCamel";
import * as labelReplacementData from '../../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/clientSettings_LabelReplacements.json'
import { LabelReplacementComponent } from "./label-replacement.component";

export default {
    title: 'QTD Components/DataExchange/LabelReplacementComponent',
    component: LabelReplacementComponent,
    decorators: [
      moduleMetadata({
        declarations: [LabelReplacementPipe,AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
    argTypes: {OnSaveClickedEvent : {action: 'OnSaveButtonClick'}, 
              OnCancelClickEvent : {action: 'OnCancelButtonClick'}, 
              OnLabelChangedEvent: {action: 'OnLabelChanged'}},
  } as Meta;

  const Template: Story<LabelReplacementComponent> = (args: LabelReplacementComponent) => ({
    props: args,
  });

  export const Default = Template.bind({});
  Default.args = {
    ClientSettings_Labels : pascalToCamel(labelReplacementData),
  }