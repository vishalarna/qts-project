import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { TrainingMapDesignComponent } from './training-map-design.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FormsModule } from '@angular/forms';
import { MatLegacyProgressBarModule as MatProgressBarModule } from '@angular/material/legacy-progress-bar';
import { MatToolbarModule } from '@angular/material/toolbar';

const routes: Routes = [
  {
    path: '',
    component: TrainingMapDesignComponent,
  },
];

@NgModule({
  declarations: [TrainingMapDesignComponent],
  imports: [
    CommonModule,
    MatCardModule,
    DragDropModule,
    LocalizeModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatSelectModule,
    FormsModule,
    MatProgressBarModule,
    MatToolbarModule,
  ],
})
export class TrainingMapDesignModule {}
