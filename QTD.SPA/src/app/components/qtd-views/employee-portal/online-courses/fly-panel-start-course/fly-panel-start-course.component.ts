import { DatePipe } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA, MatLegacyDialog as MatDialog} from '@angular/material/legacy-dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import {sideBarBackDrop, sideBarDisableClose, sideBarMode, sideBarClose, freezeMenu } from 'src/app/_Statemanagement/action/state.menutoggle';


@Component({
  selector: 'app-fly-panel-start-course',
  templateUrl: './fly-panel-start-course.component.html',
  styleUrls: ['./fly-panel-start-course.component.scss']
})
export class FlyPanelStartCourseComponent implements OnInit {

  checked = false;
  datePipe = new DatePipe('en-us');
  constructor(   private store: Store<{ toggle: string }>,
    private saveStore: Store<{ getTestInfo: any }>,
    private _router: Router,
    //public dialogRef: MatDialogRef<FlyPanelStartCourseComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {object: any}
    ) { }

  ngOnInit(): void {
    
  }

  startNewCourse() {
    this.closeDialog();
    this.store.dispatch(freezeMenu({doFreeze:false}))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));

  }
  closeDialog() {
    //this.dialogRef.close();
  }
  }
