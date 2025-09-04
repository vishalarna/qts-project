import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListUsersComponent } from './list-users.component';
import { AddEditUserComponent } from './add-edit-user/add-edit-user.component';
import { DataTablesModule } from 'angular-datatables';
import { RouterModule, Routes } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { SelectDropDownModule } from 'ngx-select-dropdown';
import { LayoutModule } from '../../layout/layout.module';

const routes: Routes = [
  {
    path: '',
    component: ListUsersComponent,
  },
];

@NgModule({
  declarations: [ListUsersComponent, AddEditUserComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    NgbDropdownModule,
    DataTablesModule,
    SelectDropDownModule,
    LayoutModule,
  ],
})
export class ListUsersModule {}
