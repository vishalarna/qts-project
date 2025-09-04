import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { DatePipe } from '@angular/common';
import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Observable } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';

@Component({
  selector: 'app-flypanel-bulk-edit-test-items',
  templateUrl: './flypanel-bulk-edit-test-items.component.html',
  styleUrls: ['./flypanel-bulk-edit-test-items.component.scss']
})
export class FlypanelBulkEditTestItemsComponent implements OnInit {

  @Output() closed = new EventEmitter<any>();
  @ViewChild('stepper') stepper!: MatStepper;
  displayedColumns = ["question","action","information"];


  stepperOrientation: Observable<StepperOrientation>;
  selectedValue = false;
  selectedEO = 0;
  datepipe = new DatePipe('en-us');

  message = "";

  step1Form = new UntypedFormGroup({});
  step2Form = new UntypedFormGroup({});

  dataSource = new MatTableDataSource<any>();

  constructor(
    private breakpointObserver: BreakpointObserver,
    private fb: UntypedFormBuilder,
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.readyForms();
  }

  readyForms() {
    this.step1Form = this.fb.group({
      changeStatus : new UntypedFormControl(false),
      status : new UntypedFormControl(false),
      changeEO : new UntypedFormControl(false),
      eoId : new UntypedFormControl('0'),
      deleteItem : new UntypedFormControl(false),
    })

    this.step2Form = this.fb.group({
      date: new UntypedFormControl(this.datepipe.transform(Date.now(), "yyyy-MM-dd"), Validators.required),
      reason: new UntypedFormControl(""),
    });
  }

  formulateMessage(){
    this.message = "";
    if(this.step1Form.get('deleteItem')?.value){
      this.message += "selecting to perform delete";
    }
    else{
      this.step1Form.get('changeEO')?.value ? this.message += 'changing the EOs, ':'';
      this.step1Form.get('changeStatus')?.value ? this.message += "changing the Question Status":"";
    }
  }

  stepChanged(event:any){
    if(event.selectedIndex === 2){
      this.formDataSource();
      this.formulateMessage();
    }
  }

  formDataSource(){
    var tempSource = [
      {
        question : "what is the color of a stoplight?",
        hasDelete : false,
        change: [
          {
            description: "Change EO",
            canPerform : true,
          },
          {
            description: "Change status to Inactive",
            canPerform : false,
          }
        ]
      },
      {
        question : "What is the color of a stoplight?",
        change : [
          {
            description: "Change EO",
            canPerform : true,
          },
          {
            description: "Change status to Inactive",
            canPerform : false,
          }
        ],
      }
    ]

    this.dataSource.data = tempSource;
  }

}
