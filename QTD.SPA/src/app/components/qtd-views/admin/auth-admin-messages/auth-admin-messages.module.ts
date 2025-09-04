import { RouterModule, Routes } from "@angular/router";
import { AuthAdminMessagesComponent } from "./auth-admin-messages.component";
import { AuthGuard } from "src/app/_Guards/auth.guard";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BaseModule } from "src/app/components/base/base.module";
import { LayoutModule } from "../../layout/layout.module";
import { MatLegacyTableModule as MatTableModule } from "@angular/material/legacy-table";
import { MatSortModule } from "@angular/material/sort";

const routes: Routes = [
    {
        path: '',
        canActivate: [AuthGuard],
        component: AuthAdminMessagesComponent,
    },
];

@NgModule({
    declarations: [AuthAdminMessagesComponent],
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
        BaseModule,
        ReactiveFormsModule,
        LayoutModule,
        MatSortModule,
        MatTableModule,
         
    ],

    exports: [AuthAdminMessagesComponent]
})
export class AuthAdminMessagesModule {

}