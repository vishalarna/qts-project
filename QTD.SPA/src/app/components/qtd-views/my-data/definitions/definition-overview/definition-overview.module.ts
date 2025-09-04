import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DefinitionOverviewComponent } from './definition-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { BaseModule } from 'src/app/components/base/base.module';


const routes: Routes = [
  {
    path: '',
    component: DefinitionOverviewComponent,
  }
 ]

@NgModule({
  declarations: [
    DefinitionOverviewComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BaseModule,
    RouterModule.forChild(routes),
  ]
})
export class DefinitionOverviewModule { }
