import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShDetailTabComponent } from './sh-detail-tab.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddShSetModule } from '../../fly-panel-add-sh-set/fly-panel-add-sh-set.module';
import { FormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

@NgModule({
  declarations: [ShDetailTabComponent],
  imports: [
    CommonModule,
    BaseModule,
    FlyPanelAddShSetModule,
    FormsModule,
    CKEditorModule
  ],
  exports: [ShDetailTabComponent],
})
export class ShDetailTabModule {}
