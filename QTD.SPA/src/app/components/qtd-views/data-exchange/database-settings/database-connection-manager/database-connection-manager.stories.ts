import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { DatabaseConnectionManagerComponent } from "./database-connection-manager.component";

export default {
    title: 'QTD Components/DataExchange/DatabaseConnectionManager',
    component: DatabaseConnectionManagerComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
  } as Meta;
  
  const Template: Story<DatabaseConnectionManagerComponent> = (args: DatabaseConnectionManagerComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
  Default.args = {
    dataSourceDatabaseConnection:
        [
            {
                id: 1,
                databaseName: "Quality Training Systems Production",
            },
            {
                id: 2,
                databaseName: "Quality Training Systems Test",
            },
            {
                id: 3,
                databaseName: "Quality Training Systems Management",
            }
        ] as any,
  };
  export const ConnectionManagerWithNoData = Template.bind({});
  ConnectionManagerWithNoData.args = {
    dataSourceDatabaseConnection: [] as any,
  }
