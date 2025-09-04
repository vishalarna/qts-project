import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { EnablingObjective_Suggestion } from 'src/app/_DtoModels/EnablingObjective_Suggestion/EnablingObjective_Suggestion';
import { EnablingObjective_SuggestionOptions } from 'src/app/_DtoModels/EnablingObjective_Suggestion/EnablingObjective_SuggestionOptions';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-flypanel-eo-add-suggestion',
  templateUrl: './flypanel-eo-add-suggestion.component.html',
  styleUrls: ['./flypanel-eo-add-suggestion.component.scss']
})
export class FlypanelEoAddSuggestionComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() eoId = "";
  editor = ckcustomBuild;

  description = "";
  number : number = 0;
  spinner = false;
  @Input() editSuggestion : EnablingObjective_Suggestion | undefined;

  constructor(private eoService : EnablingObjectivesService,
    private alert :SweetAlertService, private labelPipe: LabelReplacementPipe) { }

    ngOnInit(): void {
      if(this.editSuggestion === undefined)
      {
        this.readyNumber();
      }
      else{
        this.readyEditData();
      }
    }
  
    async transformTitle(title: string) {
      const labelName = await this.labelPipe.transform(title);
      return labelName;
    }

    async readyNumber(){
      this.number = await this.eoService.getSuggestionNumber(this.eoId);
      
    }
  
    readyEditData(){
      this.number = this.editSuggestion?.number ?? 0;
      this.description = this.editSuggestion?.description ?? "";
    }
  
    async saveSuggestion(){
      this.spinner = true;
      var options = new EnablingObjective_SuggestionOptions();
      options.description = this.description;
      options.eoId = this.eoId;
      await this.eoService.createSuggestion(this.eoId,options).then(async (_)=>{
        this.alert.successToast(await this.transformTitle('Task') + " Specific Suggestion Created");
        this.refresh.emit();
        this.closed.emit();
      }).finally(()=>{
        this.spinner = false;
      })
    }
  
    async updateSuggestion(){
      this.spinner = true;
      var options = new EnablingObjective_SuggestionOptions();
      options.description = this.description;
      await this.eoService.updateSuggestion(this.eoId, this.editSuggestion?.id,options).then((_)=>{
        this.alert.successToast("Suggestion Updated");
        this.refresh.emit();
        this.closed.emit();
      }).finally(()=>{
        this.spinner = false;
      })
    }

}
