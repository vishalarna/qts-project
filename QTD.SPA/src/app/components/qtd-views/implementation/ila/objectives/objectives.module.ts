import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { BaseModule } from 'src/app/components/base/base.module';
import { ObjectivesComponent } from './objectives.component';
import { Routes, RouterModule } from '@angular/router';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FormsModule } from '@angular/forms';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

const routes: Routes = [
  {
    path: '',
    component: ObjectivesComponent,
  },
];

@NgModule({
  declarations: [ObjectivesComponent],
  imports: [
    CommonModule,
    MatTabsModule,    
    BaseModule,
    MatMenuModule,     
    CommonModule,
    MatCardModule,
    DragDropModule,
    LocalizeModule,
    BaseModule,
    MatSelectModule,
    MatSidenavModule,
    FormsModule,
    MatExpansionModule,
    MatMenuModule
  ],
  exports: [ObjectivesComponent],
})
export class ObjectivesModule { }
