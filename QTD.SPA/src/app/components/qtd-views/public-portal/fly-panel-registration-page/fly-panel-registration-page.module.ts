import { NgModule } from "@angular/core";
import { FlyPanelRegistrationPageComponent } from "./fly-panel-registration-page.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BaseModule } from "src/app/components/base/base.module";
import { MatLegacySelectModule as MatSelectModule } from "@angular/material/legacy-select";
import { CommonModule } from "@angular/common";
import { MatIconModule } from "@angular/material/icon";

@NgModule({
    declarations: [FlyPanelRegistrationPageComponent],
    imports:[FormsModule,
             BaseModule,
             ReactiveFormsModule,
             MatSelectModule,
             CommonModule,
             MatIconModule],
    exports:[FlyPanelRegistrationPageComponent]
})
export class FlyPanelRegistrationPageModule{ 

}