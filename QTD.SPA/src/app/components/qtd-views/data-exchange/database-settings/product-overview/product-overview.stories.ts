import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { ProductOverviewComponent } from "./product-overview.component";
import * as licenseSettingData from '../../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/clientSettings_License.json';
import * as productData from '../../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/clientSettings_License_ProductInfo.json';
import { pascalToCamel } from "src/app/_Shared/Utils/PascalToCamel";
export default {
    title: 'QTD Components/DataExchange/ProductOverview',
    component: ProductOverviewComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
    argTypes: {OnClientIdChangeEvent : {action: 'OnClientIdChange'}, 
    OnActivationCodeChangeEvent : {action: 'OnActivationCodeChange'}, 
    OnSaveEvent: {action: 'OnSaveButtonClick'},
    OnCancelEvent: {action: 'OnCancelButtonClick'}},
  } as Meta;

  const Template: Story<ProductOverviewComponent> = (args: ProductOverviewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
  Default.args={
      ClientSettings_License: pascalToCamel(licenseSettingData),
    // ClientSettings_ProductInfo: pascalToCamel(productData)
  }