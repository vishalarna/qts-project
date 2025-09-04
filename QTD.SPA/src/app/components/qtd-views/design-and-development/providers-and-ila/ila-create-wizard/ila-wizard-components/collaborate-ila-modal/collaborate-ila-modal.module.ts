import { NgModule } from "@angular/core";
import { CollaborateIlaModalComponent } from "./collaborate-ila-modal.component";
import {BaseModule} from 'src/app/components/base/base.module'
import { MatLegacyDialogModule as MatDialogModule } from "@angular/material/legacy-dialog";
import { FlyPanelPositionsModule } from "src/app/components/qtd-views/implementation/positions/fly-panel-positions/fly-panel-positions.module";
import { MatLegacyTableModule as MatTableModule } from "@angular/material/legacy-table";
import { CKEditorModule } from "@ckeditor/ckeditor5-angular";
import { CommonModule } from "@angular/common";

@NgModule({
  declarations:[CollaborateIlaModalComponent],
  imports:[
    CommonModule,
    BaseModule,
    MatDialogModule,
    FlyPanelPositionsModule,
    MatTableModule,
    CKEditorModule,
  ],
  exports:[CollaborateIlaModalComponent],
})

export class CollaborateIlaModalModule{}
