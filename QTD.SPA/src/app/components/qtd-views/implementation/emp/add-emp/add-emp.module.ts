import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddEmpComponent } from './add-emp.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { LayoutModule } from '../../../layout/layout.module';
import { FlyPanelOrganizationModule } from '../../organizations/flyPanel-organizations/flyPanel-organizations.module';
import { FlyPanelPositionsModule } from '../../positions/fly-panel-positions/fly-panel-positions.module';
import { FlyPanelCertifyingBodyModule } from '../../certifying-body/fly-panel-certifying-body/fly-panel-certifying-body.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FlyPanelAddEmployeePositionModule } from '../fly-panel-add-employee-position/fly-panel-add-employee-position.module';
import { FlyPanelAddPositionModule } from '../../../my-data/positions/fly-panel-add-position/fly-panel-add-position.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelLinkEmployeeCertificationModule } from '../fly-panel-link-employee-certification/fly-panel-link-employee-certification.module';
import { FlyPanelAddOrganizationModule } from '../fly-panel-add-organization/fly-panel-add-organization.module';
import { FlyPanelLinkEmpOrganizationModule } from '../fly-panel-link-emp-organization/fly-panel-link-emp-organization.module';
import { DeleteEmpDialogueModule } from '../delete-emp-dialogue/delete-emp-dialogue.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { FlyPanelAddCertificationModule } from '../../../my-data/certifications/fly-panel-add-certification/fly-panel-add-certification.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatStepperModule } from '@angular/material/stepper';
import { FlyPanelRenewCertificationComponent } from '../fly-panel-renew-certification/fly-panel-renew-certification.component';
import { FlyPanelRenewCertificationModule } from '../fly-panel-renew-certification/fly-panel-renew-certification.module';
import { DialogueLimitEmpModule } from '../dialogue-limit-emp/dialogue-limit-emp.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatSortModule } from '@angular/material/sort';
import { DocumentTableModule } from '../../../document-storage/document-table/document-table.module';
import { FlyPanelViewEmpCertificationHistoryModule } from '../fly-panel-view-emp-certification-history/fly-panel-view-emp-certification-history.module';


const routes: Routes = [
  {
    path: '',
    component: AddEmpComponent,

  },
  {
    path: 'edit/:id',
    component: AddEmpComponent
  }
];

@NgModule({
  declarations: [AddEmpComponent],
  imports: [
    BaseModule,
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    NgbDropdownModule,
    DataTablesModule,
    FormsModule,
    ReactiveFormsModule,
    LayoutModule,
    FlyPanelOrganizationModule,
    FlyPanelPositionsModule,
    FlyPanelCertifyingBodyModule,
    BaseModule,
    MatCheckboxModule,
    CKEditorModule,
    FlyPanelAddEmployeePositionModule,
    FlyPanelAddPositionModule,
    MatTableModule,
    FlyPanelLinkEmployeeCertificationModule,
    FlyPanelAddOrganizationModule,
    FlyPanelLinkEmpOrganizationModule,
    MatPaginatorModule,
    NgxDropzoneModule,
    FlyPanelAddCertificationModule,
    FlyPanelRenewCertificationModule,
    MatMenuModule,
    MatStepperModule,
    DialogueLimitEmpModule,
    MatDialogModule,
    MatSortModule,
    DocumentTableModule,
    FlyPanelViewEmpCertificationHistoryModule
  ],
})
export class AddEmpModule { }
