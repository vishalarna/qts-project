import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '@angular/cdk/layout';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FlyPanelAddReviewersComponent } from './fly-panel-add-reviewers.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelCreateQtdUserModule } from '../fly-panel-create-qtd-user/fly-panel-create-qtd-user.module';
import { FormsModule } from '@angular/forms'; 



@NgModule({
  declarations: [FlyPanelAddReviewersComponent],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule, 
    LayoutModule,
    MatSelectModule,
    MatExpansionModule,
    MatMenuModule,
    MatCheckboxModule,
    MatIconModule,
    MatTreeModule,
    MatTableModule,
    FlyPanelCreateQtdUserModule
  ],
  exports:[FlyPanelAddReviewersComponent]
})
export class FlyPanelAddReviewersModule { }