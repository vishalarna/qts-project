import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})
export class ReportsComponent implements OnInit {
  constructor(
  ) { }

  ngOnInit() {
  }

}
