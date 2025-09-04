import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { CreateNewProgramReviewComponent } from './create-new-program-review.component';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FlyPanelLinkReviewersModule } from '../fly-panel-link-reviewers/fly-panel-link-reviewers.module';


@NgModule({
  declarations: [
    CreateNewProgramReviewComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    LayoutModule,
    MatSelectModule,
    FlyPanelLinkReviewersModule
  ],
  exports:[CreateNewProgramReviewComponent]
})
export class CreateNewProgramReviewModule { }
