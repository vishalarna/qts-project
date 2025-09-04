import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TrainingProgram_VersionTitleViewModel } from 'src/app/_DtoModels/TrainingProgram/TrainingProgram_VersionTitleViewModel';
import { TrainingProgramsService } from 'src/app/_Services/QTD/training-programs.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-overview-tp-noreviews',
  templateUrl: './fly-panel-overview-tp-noreviews.component.html',
  styleUrls: ['./fly-panel-overview-tp-noreviews.component.scss']
})
export class FlyPanelOverviewTpNoreviewsComponent implements OnInit {
  
  @Input() handleLoad: (e) => void;
  @Input() handleXClick: (e) => void;
  displayedColumns: string[] = [
    'positionName',
    'trainingProgramType',
    'programTitle',
  ];
  @ViewChild(MatSort) sort!: MatSort;
  flyPanelHeading: string;
  dataSource: MatTableDataSource<TrainingProgram_VersionTitleViewModel>=new MatTableDataSource();
  trainingPrograms: TrainingProgram_VersionTitleViewModel[];
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  }

  constructor(public flyInService: FlyInPanelService,
    private tpService: TrainingProgramsService) { }

  async ngOnInit() {
    await this.load();
  }

  _handleLoad(e){
    if (this.handleLoad &&typeof this.handleLoad === 'function') {
      this.handleLoad(e);
    }
  }

  _handleXClick(e){
    if (this.handleXClick &&typeof this.handleXClick === 'function') {
      this.handleXClick(e);
    }
  }

  async load(){
    this.flyPanelHeading = "Training Programs with no Review";
    this.trainingPrograms = await this.tpService.getTrainingProgramsWithNoReviews();
    this.dataSource.data = this.trainingPrograms;
    this.dataSource.sort = this.sort;
    this.dataSource.sortingDataAccessor=this.customSortAccessor;
  }

  xClick(){
    this.flyInService.close();
  }

  customSortAccessor = (trainingProgram: TrainingProgram_VersionTitleViewModel, column: string): string  => {
    switch (column) {
      case 'programTitle':
        return trainingProgram.programTitle + " - " + trainingProgram.version;
      default:
        return trainingProgram[column];
    }
  };
}
