import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LayoutModule } from '@angular/cdk/layout';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelViewToDosComponent } from './fly-panel-view-to-dos.component';
import { RouterModule, Routes } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';

const routes: Routes = [
  {
    path: '',
    pathMatch:'full',
    component: FlyPanelViewToDosComponent,
  }, 
];


@NgModule({
  declarations: [FlyPanelViewToDosComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatTableModule,
    MatPaginatorModule,
    BaseModule,
    LayoutModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
    MatStepperModule,
    MatSelectModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatCheckboxModule,
    MatExpansionModule,
    MatMenuModule,
    MatIconModule
  ]
})
export class FlyPanelViewToDosModule { }
