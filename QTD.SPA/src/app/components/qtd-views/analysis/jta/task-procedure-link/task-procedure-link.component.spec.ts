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

import { TaskProcedureLinkComponent } from './task-procedure-link.component';

describe('TaskProcedureLinkComponent', () => {
  let component: TaskProcedureLinkComponent;
  let fixture: ComponentFixture<TaskProcedureLinkComponent>;

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
      declarations: [TaskProcedureLinkComponent],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskProcedureLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  
  it('should not show Unlink Procedure button if none Procedure from table is checked', () => {
    component.procToUnLink = [];
    fixture.detectChanges();
    addNewEl = fixture.debugElement.query(By.css('#addNewProc'));
    unlinkEl = fixture.debugElement.query(By.css('#unlinkProc'));
    expect(addNewEl).toBeTruthy();
    expect(unlinkEl).toBeNull();
  });

  it('should not show Add new Procedure button if one or more Procedure from table is checked', () => {
    component.procToUnLink = ['Test Proc', 'Test Proc 2'];
    fixture.detectChanges();
    addNewEl = fixture.debugElement.query(By.css('#addNewProc'));
    unlinkEl = fixture.debugElement.query(By.css('#unlinkProc'));
    expect(addNewEl).toBeNull();
    expect(unlinkEl).toBeTruthy();
  });

  it('should call getProcedures method when Add new Procedure button is clicked', fakeAsync(() => {
    component.procToUnLink = [];
    fixture.detectChanges();
    spyOn(component, 'getProcedures');
    let btn = fixture.debugElement.query(By.css('#btnAddNewProc'));
    btn.nativeElement.click();
    tick();
    fixture.detectChanges();
    expect(component.getProcedures).toHaveBeenCalled();
  }));
});
