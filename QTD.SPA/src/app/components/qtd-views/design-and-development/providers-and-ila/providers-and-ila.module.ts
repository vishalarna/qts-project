import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProvidersAndIlaComponent } from './providers-and-ila.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { IlaDetailsModule } from './ila-create-wizard/ila-wizard-components/ila-details/ila-details.module';
import { FlyPanelBulkEditModule } from './fly-panel-bulk-edit/fly-panel-bulk-edit.module';
import { DeliveryMethodsResolverService } from 'src/app/_Resolvers/delivery-method.resolver';


const routes: Routes = [
  {
    path: '',
    component: ProvidersAndIlaComponent,
    children: [
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full',
      },
      {
        path: 'list',
        loadChildren: () =>
          import('./list-ila/list-ila.module').then((m) => m.ListIlaModule),
      },
      {
        path: 'create',
        loadChildren: () =>
          import('./ila-create-wizard/ila-create-wizard.module').then(
            (m) => m.IlaCreateWizardModule
          ),
        resolve:{deliveryMethods:DeliveryMethodsResolverService}
      },
    ],
  },
];

@NgModule({
  declarations: [ProvidersAndIlaComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    IlaDetailsModule,
    FlyPanelBulkEditModule,
  ],
})
export class ProvidersAndIlaModule {}
