import { Component, OnInit } from '@angular/core';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
export interface PeriodicElement {
  name: string;
  position: number;
  weight: string;
  symbol: string;
  modifiedate:string;
}

@Component({
  selector: 'app-definition-overview',
  templateUrl: './definition-overview.component.html',
  styleUrls: ['./definition-overview.component.scss']
})
export class DefinitionOverviewComponent implements OnInit {
  isLoading: boolean = false;
  catCompleted: any
  catIncompleted: any
  displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  dataSource:PeriodicElement[];
  constructor(private labelPipe: LabelReplacementPipe) { }
 
  async ngOnInit(): Promise<void> {
    this.dataSource = [
      {position: 12, name: 'John Smith', weight: 'Change ' + await this.labelPipe.transform('Instructor') + ' admin status', symbol: 'Sara Johnson',modifiedate:"25-09-1994"},
      {position: 13, name: 'Jessica Albert', weight:'Change ' + await this.labelPipe.transform('Instructor') + ' Email', symbol: 'Tara Johnson',modifiedate:"28-09-1994"},
    ];
    this.catCompleted = 10
    this. catIncompleted = 24
  }

}
