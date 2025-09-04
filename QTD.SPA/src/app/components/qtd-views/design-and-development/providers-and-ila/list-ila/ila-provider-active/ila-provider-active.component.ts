import { Component, Inject, OnInit } from '@angular/core';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';


@Component({
  selector: 'app-ila-provider-active',
  templateUrl: './ila-provider-active.component.html',
  styleUrls: ['./ila-provider-active.component.scss']
})
export class IlaProviderActiveComponent implements OnInit {

  isProviderInactive:boolean;
  isIlaInactive:boolean;
  inactiveClicked:boolean;

  header: string;
  description: string;
  cancelText: string;
  confirmText: string;

  constructor(
    private labelPipe:LabelReplacementPipe
  ) { }
  ngOnInit(): void {
    this.settingDialogue();
  }

  async settingDialogue() {
    if(this.isProviderInactive == true && this.inactiveClicked == true)
    {
      this.header = 'Make Provider Inactive';
      this.description = 'This Provider, has Active ' + await this.labelPipe.transform('ILA') + 's. Making this Provider Inactive will inactivate all linked ' + await this.labelPipe.transform('ILA') + 's. Are you sure you want to continue?';
      this.cancelText = '';
      this.confirmText = 'Yes';
    }
    else if(!this.isProviderInactive && this.inactiveClicked)
    {
      this.header = 'Make Provider Active';
      this.description = 'You are selecting to make the Provider Active. Are you sure you want to continue?';
      this.cancelText = '';
      this.confirmText = 'Yes';
    }
    else if(this.isIlaInactive && !this.inactiveClicked)
    {
      this.header = 'Make ' + await this.labelPipe.transform('ILA') + ' Inactive';
      this.description = 'You are selecting to make this ' + await this.labelPipe.transform('ILA') + ' inactive. All ' + await this.labelPipe.transform('ILA') +  ' history and ' + await this.labelPipe.transform('Employee') + ' records will be retained. Are you sure you want to continue?';
      this.cancelText = '';
      this.confirmText = 'Yes';
    }
    else if(!this.isIlaInactive && !this.inactiveClicked)
    {
      this.header = 'Make ' + await this.labelPipe.transform('ILA') +  ' Active';
      this.description = 'You are selecting to make this ' + await this.labelPipe.transform('ILA') + ' Active. Are you sure you want to continue?';
      this.cancelText = '';
      this.confirmText = 'Yes';
    }
    
  }
  
}
