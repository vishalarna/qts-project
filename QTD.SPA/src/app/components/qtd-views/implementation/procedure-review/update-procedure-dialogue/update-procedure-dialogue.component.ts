import { Component, Input, OnInit } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';

@Component({
  selector: 'app-update-procedure-dialogue',
  templateUrl: './update-procedure-dialogue.component.html',
  styleUrls: ['./update-procedure-dialogue.component.scss']
})
export class UpdateProcedureDialogueComponent implements OnInit {
  isSpinner: boolean;

  constructor(
    private procedureService:ProceduresService,
    private _router: Router,

  ) { }
  @Input() procedureReviewId: any;
  @Input() procedureTitle: any;
  dataSourceProcedureReview = new MatTableDataSource<any>();
  displayedAllColumns: string[] = [
    'employee',
    'status',
  ];
  ngOnInit(): void {
    this.getEmployees();

  }
  NavigateTOEdit(){
        this._router.navigate(['/procedure/edit/',this.procedureReviewId]);

  }
  async getEmployees() {
    
    this.isSpinner = true;
    this.procedureService
      .getLinkProcedureReviewEmp(this.procedureReviewId)
      .then((res: any) => {
    

        this.dataSourceProcedureReview.data = res;
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.isSpinner = false;
      });
  }

}
