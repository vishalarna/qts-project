import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { TrainingCreationOptions } from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import { InstructorService } from 'src/app/_Services/QTD/instructor.service';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LocationService } from 'src/app/_Services/QTD/location.service';
import { Location } from 'src/app/_DtoModels/Locations/Location';

@Component({
  selector: 'app-fly-panel-change-enrollment-settings',
  templateUrl: './fly-panel-change-enrollment-settings.component.html',
  styleUrls: ['./fly-panel-change-enrollment-settings.component.scss']
})
export class FlyPanelChangeEnrollmentSettingsComponent implements OnInit {
  location_list: Location[] = [];
  instructor_list: any[] = [];
  @Input() getEditInformation!:any;
  @Input() scheduleId!:any;
  @Output() success = new EventEmitter<any>()
  datePipe = new DatePipe('en-us');
  editTrainingForm: UntypedFormGroup = new UntypedFormGroup({
    ilaId: new UntypedFormControl('', Validators.required),
    startDate: new UntypedFormControl('', Validators.required),
    startTime: new UntypedFormControl('', Validators.required),
    endDate: new UntypedFormControl('', Validators.required),
    endTime: new UntypedFormControl('', Validators.required),
    instructorId: new UntypedFormControl('', Validators.required),
    locationId: new UntypedFormControl('', Validators.required),
    courseInstruction: new UntypedFormControl(''),

  });
  constructor(private flyPanelSrvc :FlyInPanelService,private trService:TrainingService
    , private alert: SweetAlertService, private dataBroadcastService:DataBroadcastService,
    private instructorService: InstructorService,
    private locationSevc: LocationService,) { }
  ngOnInit(): void {

   
    this.getInstructors();
    this.getLocations();
    this.editTrainingForm.patchValue({
      ilaId: this.getEditInformation.ILAId,
      startDate: this.datePipe.transform(this.getEditInformation.startDateTime, "yyyy-MM-dd",),
      startTime: this.datePipe.transform(this.getEditInformation.startDateTime, "HH:mm"),
      endDate: this.datePipe.transform(this.getEditInformation.endDateTime, "yyyy-MM-dd"),
      endTime: this.datePipe.transform(this.getEditInformation.endDateTime, "HH:mm"),
      instructorId:this.getEditInformation.instructorId,
      locationId:this.getEditInformation.locationId

    });
  }
  closeflyPanel() {
     this.flyPanelSrvc.close();
  }

  async updateData(){
  /*   var startDate = new Date(this.editTrainingForm.get('startDate')?.value);
    var startTime = this.editTrainingForm.get('startTime')?.value; */

    var updateStartDateTime = `${this.editTrainingForm.get('startDate')?.value}T${this.editTrainingForm.get('startTime')?.value}`;
    var updateEndDateTime = `${this.editTrainingForm.get('endDate')?.value}T${this.editTrainingForm.get('endTime')?.value}`;

    
    /* const [hours, minutes] = startTime.split(':');
    startDate.setHours(hours);
    startDate.setMinutes(minutes);
    var endDate = new Date(this.editTrainingForm.get('startDate')?.value);
    var endTime = this.editTrainingForm.get('startTime')?.value;
    const [hoursEnd, minutesEnd] = startTime.split(':');
    endDate.setHours(hoursEnd);
    endDate.setMinutes(minutesEnd); */
    var options = new TrainingCreationOptions();
    options.startDateTime = updateStartDateTime;
    options.endDateTime = updateEndDateTime;
    options.notes = this.editTrainingForm.get('courseInstruction')?.value;
    var startDate = new Date(options.startDateTime);
    options.startDateTime = startDate.toUTCString();
    var endDate = new Date(options.endDateTime);
    options.endDateTime = endDate.toUTCString();
    options.instructorId = this.editTrainingForm.get('instructorId')?.value;
    options.locationId = this.editTrainingForm.get('locationId')?.value;
    await this.trService.updateDateTime(this.getEditInformation.id,options).then((_)=>{
      this.alert.successToast("Time Updated For Class");
      this.dataBroadcastService.updateTime.next(null);
    }).finally(()=>{

    });
  }
  async getInstructors() {
    await this.instructorService
      .getInstructor()
      .then((res) => {
        this.instructor_list = res.filter((x) => x.active == true);
      })
      .catch((err) => {
        console.error(err);
      })

  }

  async getLocations() {
    await this.locationSevc
      .getLocation()
      .then((res) => {
        this.location_list = res.filter((x) => x.active == true);
      })
      .catch((err) => {
        console.error(err);
      })
      
  }

}
