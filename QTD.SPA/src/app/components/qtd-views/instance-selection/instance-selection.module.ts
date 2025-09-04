import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InstanceSelectionComponent } from './instance-selection.component';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import {  MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { BaseModule } from '../../base/base.module';
import { BrowserModule } from '@angular/platform-browser';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { LayoutModule } from '../layout/layout.module';


const routes: Routes = [
  {
    path: '',
    component: InstanceSelectionComponent,
  },
];

@NgModule({
  declarations: [InstanceSelectionComponent],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes),
    BaseModule,
    MatTableModule
    ,MatMenuModule,
    LayoutModule,
    MatPaginatorModule,
  ],exports:[InstanceSelectionComponent]
})
export class InstanceSelectionModule { }
