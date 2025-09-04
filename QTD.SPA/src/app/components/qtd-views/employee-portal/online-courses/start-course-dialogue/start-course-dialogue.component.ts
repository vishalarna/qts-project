import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { ClassScheduleService } from 'src/app/_Services/QTD/class-schedule.service';

@Component({
  selector: 'app-start-course-dialogue',
  templateUrl: './start-course-dialogue.component.html',
  styleUrls: ['./start-course-dialogue.component.scss']
})
export class StartCourseDialogueComponent implements OnInit {
  @Input() classSchedule_emp?:any;
  @Output() close = new EventEmitter<any>();
  public cbtScormRegistration;
  public dialog: MatDialog;
  constructor(   private store: Store<{ toggle: string }>,
    private saveStore: Store<{ getTestInfo: any }>,
    private _router: Router,
    private _classScheduleService: ClassScheduleService
    ) { }

  ngOnInit(): void {

  }

  async startNewCourse() {
      await this._classScheduleService.getCBTScormRegistrationLink(this.classSchedule_emp?.classScheduleId).then((res =>{
        this.cbtScormRegistration = res;
        window.location.href = this.cbtScormRegistration?.launchLink;
      }));
  }
  closeDialog() {
    this.close.emit("Successful");
    this.dialog.closeAll();
  }

}
