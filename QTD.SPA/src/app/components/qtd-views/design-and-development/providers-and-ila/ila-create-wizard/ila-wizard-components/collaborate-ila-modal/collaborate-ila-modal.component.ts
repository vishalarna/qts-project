import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Inject, ViewContainerRef } from '@angular/core';
import { MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA } from '@angular/material/legacy-dialog';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-collaborate-modal',
  templateUrl: './collaborate-ila-modal.component.html',
  styleUrls: ['./collaborate-ila-modal.component.scss'],
})
export class CollaborateIlaModalComponent {
  displayedColumns: string[] = ['name', 'email', 'permissions', 'actions'];
  tempDataSource: any = [
    {
      name: 'Stephanie Lynn',
      email: 'stephanie.lynn@qts.com',
      permissions: 'Edit',
      actions: '',
    },
  ];
  public Editor = ckcustomBuild;
  
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private vcf: ViewContainerRef,
    private flyPanelService: FlyInPanelService
  ) {}

  onReady(event: any) {
    
  }
}
