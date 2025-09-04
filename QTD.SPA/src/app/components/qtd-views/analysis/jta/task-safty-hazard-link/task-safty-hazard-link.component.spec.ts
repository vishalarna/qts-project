import { CommonModule } from '@angular/common';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA, DebugElement } from '@angular/core';
import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateService } from '@ngx-translate/core';
import { DataTablesModule } from 'angular-datatables';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { TaskSaftyHazardLinkComponent } from './task-safty-hazard-link.component';

describe('TaskSaftyHazardLinkComponent', () => {
  let component: TaskSaftyHazardLinkComponent;
  let fixture: ComponentFixture<TaskSaftyHazardLinkComponent>;

  let addNewEl: DebugElement;
  let unlinkEl: DebugElement;


  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        LocalizeModule,
        CommonModule,
        FormsModule,
        HttpClientTestingModule,
     LocalizeModule,
        DataTablesModule,
      ],
      declarations: [TaskSaftyHazardLinkComponent],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskSaftyHazardLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  
  it('should not show Unlink Safety Hazard button if none Safety Hazard from table is checked', () => {
    component.shToUnLink = [];
    fixture.detectChanges();
    addNewEl = fixture.debugElement.query(By.css('#addNewSh'));
    unlinkEl = fixture.debugElement.query(By.css('#unlinkSh'));
    expect(addNewEl).toBeTruthy();
    expect(unlinkEl).toBeNull();
  });

  it('should not show Add new Safety Hazard button if one or more Safety Hazard from table is checked', () => {
    component.shToUnLink = ['Test Proc', 'Test Proc 2'];
    fixture.detectChanges();
    addNewEl = fixture.debugElement.query(By.css('#addNewSh'));
    unlinkEl = fixture.debugElement.query(By.css('#unlinkSh'));
    expect(addNewEl).toBeNull();
    expect(unlinkEl).toBeTruthy();
  });

  it('should call getSHWithCategories method when Add new Safety Hazard button is clicked', fakeAsync(() => {
    component.shToUnLink = [];
    fixture.detectChanges();
    spyOn(component, 'getSHWithCategories');
    let btn = fixture.debugElement.query(By.css('#btnAddNewSh'));
    btn.nativeElement.click();
    tick();
    fixture.detectChanges();
    expect(component.getSHWithCategories).toHaveBeenCalled();
  }));
});
