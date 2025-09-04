import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { CreateTrainingMapComponent } from './create-training-map.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { LayoutModule } from '../../../layout/layout.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FlyPanelPositionsModule } from '../../../implementation/positions/fly-panel-positions/fly-panel-positions.module';
import { FlyPanelAddTrainingProgramModule } from '../../training-program/fly-panel-add-training-program/fly-panel-add-training-program.module';

const routes: Routes = [
  {
    path: '',
    component: CreateTrainingMapComponent,
  },
];

@NgModule({
  declarations: [CreateTrainingMapComponent],
  imports: [
    CommonModule,
    MatCardModule,
    LocalizeModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatSelectModule,
    MatButtonModule,
    ReactiveFormsModule,
    FormsModule,
    LayoutModule,
    BaseModule,
    MatSidenavModule,
    FlyPanelPositionsModule,
    FlyPanelAddTrainingProgramModule,
  ],
})
export class CreateTrainingMapModule {}
