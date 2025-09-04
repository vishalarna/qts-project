import { DatePipe } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { MatLegacyDialogRef as MatDialogRef, MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA } from '@angular/material/legacy-dialog';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { getTestInfo } from 'src/app/_Statemanagement/action/state.componentcommunication';
import { freezeMenu, sideBarBackDrop, sideBarClose, sideBarDisableClose } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-start-test-dialog',
  templateUrl: './start-test-dialog.component.html',
  styleUrls: ['./start-test-dialog.component.scss']
})
export class StartTestDialogComponent implements OnInit {

  checked = false;
  datePipe = new DatePipe('en-us');
  constructor(   private store: Store<{ toggle: string }>,
    private saveStore: Store<{ getTestInfo: any }>,
    private _router: Router,
    public dialogRef: MatDialogRef<StartTestDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {object: any}
    ) { }

  ngOnInit(): void {
    
    
  }

  startNewTest() {
    this.saveStore.dispatch(getTestInfo({ saveData: this.data, tabIndex: 1 ,update:false}));
    this.closeDialog();
    this.store.dispatch(freezeMenu({doFreeze:false}))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/implementation/test/start-test',this.data.object.testId]);
  }
  closeDialog() {
    this.dialogRef.close();
  }

}
