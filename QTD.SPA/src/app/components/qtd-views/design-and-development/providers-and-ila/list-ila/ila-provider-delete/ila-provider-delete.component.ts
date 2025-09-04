import { Component, OnInit } from '@angular/core';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-ila-provider-delete',
  templateUrl: './ila-provider-delete.component.html',
  styleUrls: ['./ila-provider-delete.component.scss']
})
export class IlaProviderDeleteComponent implements OnInit {
  isProviderDelete:boolean;
  isILADelete:boolean;

  header: string;
  description: string;
  cancelText: string;
  confirmText: string;
  
  constructor(private labelPipe: LabelReplacementPipe) { }

  ngOnInit(): void {
    this.settingDialogue();
  }

  async settingDialogue() {
    if(this.isProviderDelete)
    {
      this.header = 'Delete Provider';
      this.description = 'You are selecting to Delete an ' + await this.labelPipe.transform('ILA') +  ' Provider. Are you sure you want to continue?';
      this.cancelText = '';
      this.confirmText = 'Yes';
    }
    else if(!this.isProviderDelete)
    {
      this.header = 'Delete '  + await this.labelPipe.transform('ILA');
      this.description = 'You are selecting to delete the selected ' + await this.labelPipe.transform('ILA') + '. This cannot be undone. Are you sure you want to continue?';
      this.cancelText = '';
      this.confirmText = 'Yes';
    }
    
    
  }

}
