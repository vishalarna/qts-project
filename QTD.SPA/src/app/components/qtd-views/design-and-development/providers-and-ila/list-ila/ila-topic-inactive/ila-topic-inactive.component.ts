import { Component, Inject, OnInit } from '@angular/core';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-ila-topic-inactive',
  templateUrl: './ila-topic-inactive.component.html',
  styleUrls: ['./ila-topic-inactive.component.scss']
})
export class IlaTopicInactiveComponent implements OnInit {

  isTopicInactive:boolean;

  header: string;
  description: string;
  cancelText: string;
  confirmText: string;

  constructor( private labelPipe: LabelReplacementPipe,) { }

  ngOnInit(): void {
    this.settingDialogue() ;
  }

  async settingDialogue() {
    if(this.isTopicInactive)
    {
      this.header = 'Make Topic Inactive';
      this.description = 'You are selecting to make the Topic. This will remove the Topic as an option when creating an ' + await this.labelPipe.transform('ILA') + ', and will remove the Topic name from existing ' + await this.labelPipe.transform('ILA') + 's. Are you sure you want to continue?';
      this.cancelText = '';
      this.confirmText = 'Yes';
    }
    else if(!this.isTopicInactive){
      this.header = 'Make Topic Active';
      this.description = 'You are selecting to make the Topic Active. Are you sure you want to continue?';
      this.cancelText = '';
      this.confirmText = 'Yes';
    }
    
  }

 

}
