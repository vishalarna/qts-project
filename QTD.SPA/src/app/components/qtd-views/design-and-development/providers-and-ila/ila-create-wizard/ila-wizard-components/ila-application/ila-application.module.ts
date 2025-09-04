import { NgModule } from "@angular/core";
import { IlaApplicationComponent } from "./ila-application.component";
import {FormsModule,ReactiveFormsModule} from '@angular/forms'
import {BaseModule} from 'src/app/components/base/base.module'
import { MatLegacyFormFieldModule as MatFormFieldModule } from "@angular/material/legacy-form-field";
import {MatDatepickerModule} from '@angular/material/datepicker'
import {MatLegacyInputModule as MatInputModule} from "@angular/material/legacy-input"
import { MatNativeDateModule } from "@angular/material/core";
import { MatLegacyCheckboxModule as MatCheckboxModule } from "@angular/material/legacy-checkbox";
import { MatExpansionModule} from "@angular/material/expansion";
import { MatIconModule } from "@angular/material/icon";
import { CommonModule } from "@angular/common";
import { MatLegacyDialogModule as MatDialogModule } from "@angular/material/legacy-dialog";
import { CollaborateIlaModalModule } from "../collaborate-ila-modal/collaborate-ila-modal.module";
import { PublishIlaModalModule } from "../publish-ila-modal/publish-ila-modal.module";
import { MatGridListModule } from "@angular/material/grid-list";



@NgModule({
  declarations:[IlaApplicationComponent],
  imports:[
    FormsModule,
    ReactiveFormsModule,
    CollaborateIlaModalModule,
    BaseModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
    MatCheckboxModule,
    MatExpansionModule,
    MatIconModule,
    CommonModule,
    MatDialogModule,
    PublishIlaModalModule,
    MatGridListModule
  ],
  exports:[IlaApplicationComponent]
})

export class IlaApplicationModule{

}
