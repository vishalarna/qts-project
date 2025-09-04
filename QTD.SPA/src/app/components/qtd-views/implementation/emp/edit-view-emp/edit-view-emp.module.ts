import { NgModule } from '@angular/core';
import { CommonModule, TitleCasePipe } from '@angular/common';
import { EditViewEmpComponent } from './edit-view-emp.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { NgbDropdownModule, NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { FormsModule } from '@angular/forms';
import { LayoutModule } from '../../../layout/layout.module';
import { FlyPanelEmpPositionModule } from '../emp-position/fly-panel-emp-position/fly-panel-emp-position.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelEmpCertificationModule } from '../emp-certification/fly-panel-emp-certification/fly-panel-emp-certification.module';
import { FlyPanelOrganizationModule } from '../../organizations/flyPanel-organizations/flyPanel-organizations.module';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';

const routes: Routes = [
  {
    path: '',
    component: EditViewEmpComponent,
  },
];

@NgModule({
  declarations: [EditViewEmpComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    DataTablesModule,
    LayoutModule,
    FlyPanelEmpPositionModule,
    FlyPanelEmpCertificationModule,
    FlyPanelOrganizationModule,
    MatMenuModule,
    BaseModule,
    MatTabsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
  ],
  providers: [TitleCasePipe],
})
export class EditViewEmpModule {}
