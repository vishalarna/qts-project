import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard'
import { ReportsComponent } from './reports.component';
import { EditReportComponent } from './edit-report/edit-report.component';
import { ReportsViewComponent } from './reports-view/reports-view.component';
import { ReportViewComponent } from './report-view/report-view.component';


const routes: Routes = [
    {
        path: '',
        canActivate: [AuthGuard, RouteGuard],
        component: ReportsViewComponent
    },
    {
        path: 'create/:id',
        canActivate: [AuthGuard, RouteGuard],
        component: EditReportComponent,
        loadChildren: () =>
            import('./edit-report/edit-report.module').then(
                (m) => m.EditReportModule
            )
    },
    {
        path: 'update/:id',
        canActivate: [AuthGuard, RouteGuard],
        component: EditReportComponent,
        loadChildren: () =>
            import('./edit-report/edit-report.module').then(
                (m) => m.EditReportModule
            )
    },
    {
        path: 'view',
        canActivate: [AuthGuard, RouteGuard],
        component: ReportViewComponent,
        loadChildren: () =>
            import('./report-view/report-view.module').then(
                (m) => m.ReportViewModule
            )
    },

];

@NgModule({ imports: [RouterModule.forChild(routes)], exports: [RouterModule] })
export class ReportsRoutingModule { }