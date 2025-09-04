import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from './button/button.component';
import { InputErrorComponent } from './input-error/input-error.component';
import { LabelComponent } from './label/label.component';
import { LinkComponent } from './link/link.component';
import { PasswordComponent } from './password/password.component';
import { TextboxComponent } from './textbox/textbox.component';
import { IconComponent } from './icon/icon.component';
import { SpinnerComponent } from './spinner/spinner.component';
import { RouterModule } from '@angular/router';
import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MenuItemComponent } from './menu-item/menu-item.component';
import { AddNewMenuItemComponent } from './add-new-menu-item/add-new-menu-item.component';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { CardComponent } from './card/card.component';
import { DateComponent } from './date/date.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { DataLoaderComponent } from './data-loader/data-loader.component';
import { MatLegacyProgressSpinnerModule as MatProgressSpinnerModule } from '@angular/material/legacy-progress-spinner';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { TextareaComponent } from './textarea/textarea.component';
import { MatNativeDateModule } from '@angular/material/core';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { QTDDialogueComponent } from './qtd-dialogue/qtd-dialogue.component';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { ChildNavBarComponent } from './child-nav-bar/child-nav-bar.component';
import { EmailEditorComponent } from './email-editor/email-editor.component';
import { LabelReplacementPipe } from '../../_Pipes/label-replacement.pipe';
import { DateFormatPipe } from 'src/app/_Pipes/date-format.pipe';
import { TreeItemSelectorComponent } from './tree-item-selector/tree-item-selector.component';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { ResizableTableModule } from 'src/app/_Shared/directives/resizable-table.module';
import { ResizableTableDirective } from 'src/app/_Shared/directives/resizable-table.directive';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    DragDropModule,
    MatSidenavModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,ReactiveFormsModule,
    MatTooltipModule,
    ResizableTableModule
  ],
  declarations: [
    ButtonComponent,
    TextboxComponent,
    InputErrorComponent,
    PasswordComponent,
    LabelComponent,
    LinkComponent,
    IconComponent,
    SpinnerComponent,
    MenuItemComponent,
    AddNewMenuItemComponent,
    CardComponent,
    TextareaComponent,
    DateComponent,
    DataLoaderComponent,
    QTDDialogueComponent,
    ChildNavBarComponent,
    EmailEditorComponent,
    LabelReplacementPipe,
    DateFormatPipe,
    TreeItemSelectorComponent,
    DynamicLabelReplacementPipe,
  ],
  exports: [
    ButtonComponent,
    TextboxComponent,
    InputErrorComponent,
    PasswordComponent,
    LabelComponent,
    LinkComponent,
    IconComponent,
    SpinnerComponent,
    MenuItemComponent,
    AddNewMenuItemComponent,
    CardComponent,
    DateComponent,
    DataLoaderComponent,
    TextareaComponent,
    QTDDialogueComponent,
    ChildNavBarComponent,
    EmailEditorComponent,
    LabelReplacementPipe,
    DateFormatPipe,
    TreeItemSelectorComponent,
    DynamicLabelReplacementPipe,
    ResizableTableDirective
],
})
export class BaseModule {}
