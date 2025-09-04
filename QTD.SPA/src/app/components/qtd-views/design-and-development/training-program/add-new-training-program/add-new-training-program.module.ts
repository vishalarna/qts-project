import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddNewTrainingProgramComponent } from './add-new-training-program.component';
import { RouterModule, Routes } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { LayoutModule } from '@angular/cdk/layout';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelTrainingprogramPositionLinkComponent } from '../fly-panel-trainingprogram-position-link/fly-panel-trainingprogram-position-link.component';
import { FlyPanelTrainingprogramPositionLinkModule } from '../fly-panel-trainingprogram-position-link/fly-panel-trainingprogram-position-link.module';
import { FlyPanelTrainingprogramLinkILaModule } from '../fly-panel-trainingprogram-link-ila/fly-panel-trainingprogram-link-ila.module';
import { MatLegacyPaginator as MatPaginator, MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { FlyPanelPublishTrainingProgramModule } from '../fly-panel-publish-training-program/fly-panel-publish-training-program.module';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';

const routes: Routes = [
  {
    path: '',
    pathMatch:'full',
    component: AddNewTrainingProgramComponent,
  },
  {
    path: 'edit/:id',
    pathMatch:'full',
    component: AddNewTrainingProgramComponent
  },
  {
    path: 'copy/:id',
    pathMatch:'full',
    component: AddNewTrainingProgramComponent
  },
  
];


@NgModule({
  declarations: [
    AddNewTrainingProgramComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatToolbarModule,
    BaseModule,
    LayoutModule,
    MatDialogModule,
    MatToolbarModule,
    MatStepperModule,
    ReactiveFormsModule,
    MatTableModule,
    MatSelectModule,
    MatCheckboxModule,
    FlyPanelTrainingprogramPositionLinkModule,
    FlyPanelTrainingprogramLinkILaModule,
    FormsModule,
    MatPaginatorModule,
    FlyPanelPublishTrainingProgramModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule 
  ]
})
export class AddNewTrainingProgramModule { }
