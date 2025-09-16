import { FlyPanelStatisticsModule } from './../fly-panel-statistics/fly-panel-statistics.module';
import { FlyPanelVersionHistoryModule } from './../fly-panel-version-history/fly-panel-version-history.module';
import { FlyPanelTopicModule } from './../fly-panel-topic/fly-panel-topic.module';
import { FlyPanelMetaIlaModule } from './../fly-panel-meta-ila/fly-panel-meta-ila.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IlaProviderDeleteComponent } from './ila-provider-delete/ila-provider-delete.component';
import { IlaProviderActiveComponent } from './ila-provider-active/ila-provider-active.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListIlaComponent } from './list-ila.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelBulkEditModule } from '../fly-panel-bulk-edit/fly-panel-bulk-edit.module';
import { MatIconModule } from '@angular/material/icon';
import { FlyPanelProviderModule } from '../fly-panel-provider/fly-panel-provider.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { IlaTopicDeleteComponent } from './ila-topic-delete/ila-topic-delete.component';
import { IlaTopicInactiveComponent } from './ila-topic-inactive/ila-topic-inactive.component';
import { SortPipe } from './sort.pipe';
import { FilterPipe } from './filter.pipe';
import { IlaFilterFlypanelModule } from './ila-filter-flypanel/ila-filter-flypanel.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { FlyPanelAddBasicIlaModule } from '../fly-panel-add-basic-ila/fly-panel-add-basic-ila.module';

const routes: Routes = [
  {
    path: '',
    component: ListIlaComponent,
  },
];

@NgModule({
  declarations: [ListIlaComponent,IlaProviderActiveComponent,IlaProviderDeleteComponent, IlaTopicDeleteComponent, IlaTopicInactiveComponent,SortPipe,FilterPipe],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    LayoutModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatMenuModule,
    MatCheckboxModule,
    BaseModule,
    FlyPanelBulkEditModule,
    MatIconModule,
    FlyPanelProviderModule,
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule,
    FlyPanelMetaIlaModule,
    FlyPanelTopicModule,
    FlyPanelVersionHistoryModule,
    FlyPanelStatisticsModule,
    IlaFilterFlypanelModule,
    MatTooltipModule,
    FlyPanelAddBasicIlaModule
  ],
})
export class ListIlaModule {}
