import { RouterModule, Routes } from "@angular/router";
import { PublicRequestsComponent } from "./public-requests.component";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { LayoutModule } from "../../layout/layout.module";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatLegacyTabsModule as MatTabsModule } from "@angular/material/legacy-tabs";
import { MatLegacySelectModule as MatSelectModule } from "@angular/material/legacy-select";
import { BaseModule } from "src/app/components/base/base.module";
import { MatLegacyTooltipModule as MatTooltipModule } from "@angular/material/legacy-tooltip";
import { MatLegacyMenuModule as MatMenuModule } from "@angular/material/legacy-menu";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatLegacyTableModule as MatTableModule } from "@angular/material/legacy-table";
import { DisclaimerDialogModule } from "../../employee-portal/evaluation/disclaimer-dialog/disclaimer-dialog.module";
import { MatSortModule } from "@angular/material/sort";
import { MatIconModule } from "@angular/material/icon";

const routes: Routes = [
  {
    path:'',
    component:PublicRequestsComponent,
  },
]

@NgModule({
    declarations:[PublicRequestsComponent],
    imports: [
            CommonModule,
            RouterModule.forChild(routes),
            LayoutModule,
            MatSidenavModule,
            MatTabsModule,
            MatSelectModule,           
            BaseModule,
            MatTooltipModule,
            MatMenuModule,
            MatToolbarModule,
            MatTableModule,
            DisclaimerDialogModule,
            MatSortModule,
            MatIconModule
    ],
    exports:[PublicRequestsComponent]
}) 
export class PublicRequestsComponentModule{}