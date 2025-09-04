import { NgModule } from "@angular/core";
import { FlyPanelPublicClassDetailViewComponent } from "./fly-panel-public-class-detail-view.component";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { BaseModule } from "src/app/components/base/base.module";
import { MatLegacyCheckboxModule as MatCheckboxModule } from "@angular/material/legacy-checkbox";
import { MatLegacyTableModule as MatTableModule } from "@angular/material/legacy-table";
import { MatLegacyDialogModule as MatDialogModule } from "@angular/material/legacy-dialog";
import { RouterModule } from "@angular/router";
import { CalendarModule, DateAdapter } from "angular-calendar";
import { adapterFactory } from "angular-calendar/date-adapters/date-fns";
import { MatLegacyMenuModule as MatMenuModule } from "@angular/material/legacy-menu";

@NgModule({
  declarations: [FlyPanelPublicClassDetailViewComponent],
  imports: [ CommonModule,
      FormsModule,
      BaseModule,
      MatCheckboxModule,
      MatTableModule,
      MatDialogModule,
      RouterModule,CalendarModule.forRoot({
            provide: DateAdapter,
            useFactory: adapterFactory,
          }),
      MatMenuModule
      ],
 exports:[FlyPanelPublicClassDetailViewComponent]
})
export class FlyPanelPublicClassDetailViewModule { }
