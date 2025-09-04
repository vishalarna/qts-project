import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ILAComponent } from './ila.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FormsModule } from '@angular/forms';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatLegacyProgressBarModule as MatProgressBarModule } from '@angular/material/legacy-progress-bar';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { CommonModule } from '@angular/common';
import { PrerequisitesModule } from './prerequisites/prerequisites.module';
import { ProceduresModule } from './procedures/procedures.module';
import { ObjectivesModule } from './objectives/objectives.module';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';

const routes: Routes = [
  {
    path: '',
    component: ILAComponent,
  },
];

@NgModule({
  declarations: [ILAComponent],
  imports: [
    MatCardModule,
    DragDropModule,
    LocalizeModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatSelectModule,
    FormsModule,
    MatProgressBarModule,
    CommonModule,
    PrerequisitesModule,
    ObjectivesModule,
    ProceduresModule,
    MatTabsModule,
  ],
})
export class ILAModule {}
