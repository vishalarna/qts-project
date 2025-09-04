import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelAddTrainingProgramComponent } from './fly-panel-add-training-program.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

@NgModule({
  declarations: [FlyPanelAddTrainingProgramComponent],
  imports: [
    CommonModule,
    MatCardModule,
    DragDropModule,
    LocalizeModule,
    BaseModule,
    MatSelectModule,
    MatSidenavModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  exports: [FlyPanelAddTrainingProgramComponent],
})
export class FlyPanelAddTrainingProgramModule {}
