import { CommonModule } from '@angular/common';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA, DebugElement } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateService } from '@ngx-translate/core';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { SubdutyArea } from 'src/app/_DtoModels/SubdutyArea/SubdutyArea';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { TaskDetailComponent } from './task-detail.component';

describe('TaskDetailComponent', () => {
  let component: TaskDetailComponent;
  let fixture: ComponentFixture<TaskDetailComponent>;
  let eoTabEl: DebugElement;
  let taskTabEl: DebugElement;
  let procTabEl: DebugElement;
  let shTabEl: DebugElement;
  let ojtTabEl: DebugElement;
  let notaskMsgEl: DebugElement;
  let noTaskMsg = 'Please Select Task to Edit/View Detail';
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        LocalizeModule,
        CommonModule,
        FormsModule,
        HttpClientTestingModule,
     LocalizeModule,
      ],
      declarations: [TaskDetailComponent],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskDetailComponent);
    component = fixture.componentInstance;
    component.taskDetail = new Task();

    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should show message to select task if no task is selected', () => {
    component.taskDetail = undefined;
    
    fixture.detectChanges();
    notaskMsgEl = fixture.debugElement.query(By.css('#notaskMsg'));
    expect(notaskMsgEl).toBeTruthy();
    expect(notaskMsgEl.nativeElement.textContent.trim().toLowerCase()).toEqual(
      noTaskMsg.trim().toLowerCase()
    );
  });

  it('should show task detail Tab page by default', () => {
    taskTabEl = fixture.debugElement.query(By.css('#taskTab'));
    expect(taskTabEl).toBeTruthy();
  });

  it('should show enabling objective Tab page upon selecting and hide other tab pages', () => {
    component.activeTab = 'eo';
    fixture.detectChanges();

    taskTabEl = fixture.debugElement.query(By.css('#taskTab'));
    eoTabEl = fixture.debugElement.query(By.css('#eoTab'));
    procTabEl = fixture.debugElement.query(By.css('#procTab'));
    shTabEl = fixture.debugElement.query(By.css('#shTab'));
    ojtTabEl = fixture.debugElement.query(By.css('#ojtTab'));

    expect(taskTabEl).toBeNull();
    expect(eoTabEl).toBeTruthy();
    expect(procTabEl).toBeNull();
    expect(shTabEl).toBeNull();
    expect(ojtTabEl).toBeNull();
  });

  it('should show procedures Tab page upon selecting and hide other tab pages', () => {
    component.activeTab = 'procedures';
    fixture.detectChanges();

    taskTabEl = fixture.debugElement.query(By.css('#taskTab'));
    eoTabEl = fixture.debugElement.query(By.css('#eoTab'));
    procTabEl = fixture.debugElement.query(By.css('#procTab'));
    shTabEl = fixture.debugElement.query(By.css('#shTab'));
    ojtTabEl = fixture.debugElement.query(By.css('#ojtTab'));

    expect(taskTabEl).toBeNull();
    expect(eoTabEl).toBeNull();
    expect(procTabEl).toBeTruthy();
    expect(shTabEl).toBeNull();
    expect(ojtTabEl).toBeNull();
  });

  it('should show safety hazard Tab page upon selecting and hide other tab pages', () => {
    component.activeTab = 'saftyhazards';
    fixture.detectChanges();

    taskTabEl = fixture.debugElement.query(By.css('#taskTab'));
    eoTabEl = fixture.debugElement.query(By.css('#eoTab'));
    procTabEl = fixture.debugElement.query(By.css('#procTab'));
    shTabEl = fixture.debugElement.query(By.css('#shTab'));
    ojtTabEl = fixture.debugElement.query(By.css('#ojtTab'));

    expect(taskTabEl).toBeNull();
    expect(eoTabEl).toBeNull();
    expect(procTabEl).toBeNull();
    expect(shTabEl).toBeTruthy();
    expect(ojtTabEl).toBeNull();
  });

  it('should show OJT Tab page upon selecting and hide other tab pages', () => {
    component.activeTab = 'ojt';
    fixture.detectChanges();

    taskTabEl = fixture.debugElement.query(By.css('#taskTab'));
    eoTabEl = fixture.debugElement.query(By.css('#eoTab'));
    procTabEl = fixture.debugElement.query(By.css('#procTab'));
    shTabEl = fixture.debugElement.query(By.css('#shTab'));
    ojtTabEl = fixture.debugElement.query(By.css('#ojtTab'));

    expect(taskTabEl).toBeNull();
    expect(eoTabEl).toBeNull();
    expect(procTabEl).toBeNull();
    expect(shTabEl).toBeNull();
    expect(ojtTabEl).toBeTruthy();
  });
});
