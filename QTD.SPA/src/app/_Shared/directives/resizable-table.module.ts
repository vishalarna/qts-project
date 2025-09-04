import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResizableTableDirective } from 'src/app/_Shared/directives/resizable-table.directive';

@NgModule({
  declarations: [ResizableTableDirective],
  imports: [
    CommonModule
  ],
  exports:[ResizableTableDirective]
})
export class ResizableTableModule {}