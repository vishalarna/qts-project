import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartCourseDialogueComponent } from './start-course-dialogue.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    StartCourseDialogueComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatDialogModule,
    MatCheckboxModule,
    FormsModule

  ],
  exports:[StartCourseDialogueComponent]
})
export class StartCourseDialogueModule { }
