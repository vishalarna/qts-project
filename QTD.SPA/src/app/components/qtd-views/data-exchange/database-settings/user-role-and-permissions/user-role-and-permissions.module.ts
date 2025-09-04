import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserRoleAndPermissionsComponent } from './user-role-and-permissions.component';
import {BaseModule} from "../../../../base/base.module";
import {MatLegacyTabsModule as MatTabsModule} from "@angular/material/legacy-tabs";

@NgModule({
  declarations: [
    UserRoleAndPermissionsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    MatTabsModule,
  ],
  exports: [
    UserRoleAndPermissionsComponent
  ]
})
export class UserRoleAndPermissionsModule { }
