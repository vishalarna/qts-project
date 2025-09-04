import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CbtManagerComponent } from '../cbt-manager/cbt-manager.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { CbtDemoPageComponent } from './cbt-demo-page.component';
import { CbtDemoRoutingModule } from './cbt-demo-routing-module';
import { CbtManagerModule } from '../cbt-manager/cbt-manager.module';

@NgModule({
  declarations: [
    CbtDemoPageComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    CKEditorModule,
    BaseModule,
    MatRadioModule,
    MatSelectModule,
    CbtDemoRoutingModule,
    CbtManagerModule
  ],
  exports: [
    CbtDemoPageComponent
  ]
})

export class CbtDemoPageModule { }
