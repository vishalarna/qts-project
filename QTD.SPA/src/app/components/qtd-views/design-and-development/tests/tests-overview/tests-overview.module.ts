import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestsOverviewComponent } from './tests-overview.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { TestSortPipe } from './test-sort.pipe';
import { TestFilterPipe } from './test-filter.pipe';
import { FlypanelChangeIlaModule } from '../flypanel-change-ila/flypanel-change-ila.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip'
import { FlyPanelTestListModule } from '../fly-panel-test-list/fly-panel-test-list.module';


const routes: Routes = [
  {
    path: '',
    component: TestsOverviewComponent,
  }
 ]

@NgModule({
  declarations: [TestsOverviewComponent, TestSortPipe, TestFilterPipe],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    LayoutModule,
    MatInputModule,
    MatIconModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatMenuModule,
    FormsModule,
    ReactiveFormsModule,
    FlypanelChangeIlaModule,
    MatTooltipModule,
    FlyPanelTestListModule
  ],
  exports: [TestsOverviewComponent]
})
export class TestsOverviewModule { }
