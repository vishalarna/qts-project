import { MatDrawer } from '@angular/material/sidenav';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { TranslateService } from '@ngx-translate/core';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { TemplatePortal } from '@angular/cdk/portal';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TrainingProgramsService } from 'src/app/_Services/QTD/training-programs.service';
import { TrainingProgram } from 'src/app/_DtoModels/TrainingProgram/TrainingProgram';
import { Router } from '@angular/router';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';

@Component({
  selector: 'app-create-training-map',
  templateUrl: './create-training-map.component.html',
  styleUrls: ['./create-training-map.component.scss'],
})
export class CreateTrainingMapComponent implements OnInit {
  position: Position[] = [];
  training_program: TrainingProgram[] = [];
  version_number: any[] = [];
  year: any[] = [];
  selectedTrainingProgram: string;
  selectedPositionId: string;
  selectedVersionNumber: string;
  selectedYear: string;
  valueselected: string;
  defaultvalue: any;
  defaultvalueversion: any;

  constructor(
    private dataBroadcastService: DataBroadcastService,
    private positionService: PositionsService,
    private alertService: SweetAlertService,
    private translate: TranslateService,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    public router: Router,
    private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe
  ) {}

  ngOnInit(): void {
    //training map list

    //version number list
    this.version_number = [
      { title: 'Version XI . 21 Nov 2021 - N/A' },
      { title: 'Version XI . 22 Nov 2021 - N/A' },
      { title: 'Version XI . 23 Nov 2021 - N/A' },
    ];

    //year
    this.year = [
      { title: new Date().getFullYear() },
      { title: 2021 },
      { title: 2020 },
      { title: 2019 },
    ];

    //default values
    this.defaultvalue = this.year[0].title;
    this.defaultvalueversion = this.version_number[0].title;

    //for dynamic position dropdown
    this.positionService.getAllWithoutIncludes().then((i) => {
      this.position = i;
    });
  }

  async getTrainingPrograms() {
    await this.positionService
      .getTrainingPrograms(this.selectedPositionId)
      .then((res) => {
        this.training_program = res;
        this.training_program.forEach((x) => {
          
          if (x.programType.toLowerCase() == 'i') {
            x.programTitle = 'Initial Training';
          } else {
            x.programTitle = 'Continous Training';
          }
        });
      });
  }

  async openFlyInPanel(templateRef: any, ref: any) {
    if (ref == 'tp' && !this.selectedPositionId) {
      this.alertService.errorAlert(
        'Error',
       await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.PositionIdError'))
      );
      return;
    }

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  GoToDesignBoard() {
    this.router.navigate(['dnd/trainingmap/design']);
  }
}
