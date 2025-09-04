import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PreviewIlaComponent } from './preview-ila.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [PreviewIlaComponent],
  imports: [CommonModule, MatCheckboxModule,BaseModule],
  exports: [PreviewIlaComponent],
})
export class PreviewIlaModule {}
