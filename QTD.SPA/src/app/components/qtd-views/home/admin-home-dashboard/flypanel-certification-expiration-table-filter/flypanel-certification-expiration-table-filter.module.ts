import { NgModule } from "@angular/core";
import { FlypanelCertificationExpirationTableFilterComponent } from "./flypanel-certification-expiration-table-filter.component";
import { CommonModule } from "@angular/common";
import { LayoutModule } from "../../../layout/layout.module";
import { MatLegacySelectModule as MatSelectModule } from "@angular/material/legacy-select";
import { BaseModule } from "src/app/components/base/base.module";
import { MatLegacyOptionModule as MatOptionModule } from "@angular/material/legacy-core";
import { MatLegacyFormFieldModule as MatFormFieldModule } from "@angular/material/legacy-form-field";


@NgModule({
    declarations:[FlypanelCertificationExpirationTableFilterComponent],
    imports: [
        CommonModule,
        LayoutModule,
        MatSelectModule,           
        BaseModule,
        MatOptionModule,
        MatFormFieldModule
    ],
    exports:[FlypanelCertificationExpirationTableFilterComponent]
}) 
export class FlypanelCertificationExpirationTableFilterModule{}