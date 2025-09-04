import { NgModule } from "@angular/core";
import { AdminMessagesComponent } from "./admin-messages.component";
import { CommonModule } from "@angular/common";
import { BaseModule } from "../../base/base.module";

@NgModule({
    declarations:[AdminMessagesComponent],
    imports:[CommonModule,
              BaseModule
    ],
    exports:[AdminMessagesComponent]
})
export class AdminMessagesComponentModule{}