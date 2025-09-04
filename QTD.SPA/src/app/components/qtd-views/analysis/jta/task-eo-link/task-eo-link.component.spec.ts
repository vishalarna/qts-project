import { CommonModule } from '@angular/common';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA, DebugElement } from '@angular/core';
import {
  async,
  ComponentFixture,
  fakeAsync,
  TestBed,
  tick,
  waitForAsync,
} from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateService } from '@ngx-translate/core';
import { DataTablesModule } from 'angular-datatables';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { TaskEOLinkComponent } from './task-eo-link.component';

describe('TaskEOLinkComponent', () => {
  let component: TaskEOLinkComponent;
  let fixture: ComponentFixture<TaskEOLinkComponent>;
  let addNewEoEl: DebugElement;
  let unlinkEoEl: DebugElement;

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
      declarations: [TaskEOLinkComponent],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskEOLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should not show Unlink Enabling Objective button if none Enabling Objective from table is checked', () => {
    component.EOToUnLink = [];
    fixture.detectChanges();
    addNewEoEl = fixture.debugElement.query(By.css('#addNewEO'));
    unlinkEoEl = fixture.debugElement.query(By.css('#unlinkEO'));
    expect(addNewEoEl).toBeTruthy();
    expect(unlinkEoEl).toBeNull();
  });

  it('should not show Add new Enabling Objective button if one or more Enabling Objective from table is checked', () => {
    component.EOToUnLink = ['Test EO', 'Test EO 2'];
    fixture.detectChanges();
    addNewEoEl = fixture.debugElement.query(By.css('#addNewEO'));
    unlinkEoEl = fixture.debugElement.query(By.css('#unlinkEO'));
    expect(addNewEoEl).toBeNull();
    expect(unlinkEoEl).toBeTruthy();
  });
});
