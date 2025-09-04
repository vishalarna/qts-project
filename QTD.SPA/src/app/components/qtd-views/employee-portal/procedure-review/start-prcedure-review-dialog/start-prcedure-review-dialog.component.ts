import { DatePipe } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA, MatLegacyDialogRef as MatDialogRef } from '@angular/material/legacy-dialog';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { freezeMenu, sideBarBackDrop, sideBarClose, sideBarDisableClose } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-start-prcedure-review-dialog',
  templateUrl: './start-prcedure-review-dialog.component.html',
  styleUrls: ['./start-prcedure-review-dialog.component.scss']
})
export class StartPrcedureReviewDialogComponent implements OnInit {

  checked = false;
  datePipe = new DatePipe('en-us');
  constructor(   private store: Store<{ toggle: string }>,
    private saveStore: Store<{ getTestInfo: any }>,
    private _router: Router,
    public dialogRef: MatDialogRef<StartPrcedureReviewDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {object: any}
    ) { }

  ngOnInit(): void {
    
  }

  startNewTest() {
    this.closeDialog();
    this.store.dispatch(freezeMenu({doFreeze:false}))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/emp/procedure-review/start-review',this.data.object.procedureReviewId]);
  }
  closeDialog() {
    this.dialogRef.close();
  }

}
