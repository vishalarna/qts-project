import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { InstructorService } from 'src/app/_Services/QTD/instructor.service';

@Component({
  selector: 'app-fly-panel-filter-instructor',
  templateUrl: './fly-panel-filter-instructor.component.html',
  styleUrls: ['./fly-panel-filter-instructor.component.scss']
})
export class FlyPanelFilterInstructorComponent implements OnInit {
  showSpinner = false;
  instructor_list: any[] = [];
  @Output() idSelected = new EventEmitter<any>();
  @Output() insSelected = new EventEmitter<any>();
  @Output() closed = new EventEmitter<any>();;
  selectedText: string;
  instructorForm: UntypedFormGroup = new UntypedFormGroup({
    Instructor: new UntypedFormControl('', Validators.required),
  });
  constructor(  private instructorService: InstructorService,) { }

  ngOnInit(): void {
    this.getInstructors();
  }


  async getInstructors() {
    await this.instructorService
      .getInstructor()
      .then((res) => {
        
        this.instructor_list = res;
        this.instructor_list=this.instructor_list.filter(x=>x.active===true);
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }
  
  closeProvider() {
    this.closed.emit('fp-add-provider-closed');
  }

  selectInstructor(){
    this.idSelected.emit(this.instructorForm.get('Instructor')?.value);

  }
  
  onOptionSelection(event: any): void {
    debugger
    this.selectedText = event.source.triggerValue;
  }
  selectInstructorName(){
    this.insSelected.emit(this.selectedText);

  }

}
