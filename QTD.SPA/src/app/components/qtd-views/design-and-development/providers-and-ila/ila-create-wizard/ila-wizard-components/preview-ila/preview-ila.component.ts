import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { IlaService } from 'src/app/_Services/QTD/ila.service';

@Component({
  selector: 'app-preview-ila',
  templateUrl: './preview-ila.component.html',
  styleUrls: ['./preview-ila.component.scss']
})
export class PreviewIlaComponent implements OnInit {

  @Input() ilaId:number;
  isPartialCredit: string;
  nercCertLink:any;
  simulationsLink:any;
  standardHours:any;
  trainingTopics:any[];
  ilaPreviewDetails:any;
  isLoading:boolean;
  constructor(private datePipe: DatePipe, private ilaService:IlaService ) { }

  ngOnInit() {
    this.loadAsync();
  }

  async loadAsync(){
    this.isLoading = true;
    await this.getILAPreviewData();
    await this.getILATrainingTopics();
    this.isLoading = false;
  }

  async getILAPreviewData(){
    if(this.ilaId){
      this.ilaPreviewDetails = await this.ilaService.getILAPreviewDetailAsync(this.ilaId);
    }
  }

  async getILATrainingTopics(){
    this.trainingTopics = await this.ilaService.getILATrainingTopicsAsync(this.ilaId);
  }
}
