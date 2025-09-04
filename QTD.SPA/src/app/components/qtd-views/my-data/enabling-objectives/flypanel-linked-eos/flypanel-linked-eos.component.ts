import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { LinkedEOVM } from 'src/app/_DtoModels/EnablingObjective/LinkedEOVM';
import { Test } from 'src/app/_DtoModels/Test/Test';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';

@Component({
  selector: 'app-flypanel-linked-eos',
  templateUrl: './flypanel-linked-eos.component.html',
  styleUrls: ['./flypanel-linked-eos.component.scss']
})
export class FlypanelLinkedEosComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Input() Id: any; //! Use this Id to fetch the linked procedures and map into linkedProcedures and show the data in html then
  @Input() selectedTypeNumber = "";
  //! Pass the Title as input from selected Tab
  @Input() Title: string;
  @Input() selectedType: string;
  linkedEos: EnablingObjective[] = [];

  linkedTests : Test[];
  constructor(
    private eoService: EnablingObjectivesService,
  ) { }

  ngOnInit(): void {
    this.readyData();
  }

  async readyData() {
    var options = new LinkedEOVM();
    options.id = this.Id;
    switch (this.selectedType.trim().toLowerCase()) {
      case 'task':
        options.type = "task";
        break;
      case 'safety hazard':
        options.type = "sh";
        break;
      case 'regulation':
        options.type = "rr";
        break;
      case 'ila':
        options.type = "ila";
        break;
      case 'test question':
        options.type = "tq";
        break;
      case 'procedure':
        options.type = 'proc';
        break;
      case 'position':
        options.type = 'pos';
        break;
      case 'employee':
        options.type = 'emp';
        break;
    }
    if (this.selectedType.trim().toLowerCase() === 'test') {
      this.linkedTests = await this.eoService.getTestsTestItemIsLinkedTo(this.Id);
      
    }
    else {
      this.linkedEos = await this.eoService.getLinkedEOS(options);
      
    }
  }

}
