import { CommonModule } from '@angular/common';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA, DebugElement } from '@angular/core';
import {
  ComponentFixture,
  fakeAsync,
  TestBed,
  tick,
} from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateService } from '@ngx-translate/core';
import { DataTablesModule } from 'angular-datatables';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

import { TaskPositionLinkComponent } from './task-position-link.component';

describe('TaskPositionLinkComponent', () => {
  let component: TaskPositionLinkComponent;
  let fixture: ComponentFixture<TaskPositionLinkComponent>;
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
      declarations: [TaskPositionLinkComponent],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskPositionLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should not show Unlink Position button if none Position from table is checked', () => {
    component.PosToUnlink = [];
    fixture.detectChanges();
    addNewEl = fixture.debugElement.query(By.css('#addNewPos'));
    unlinkEl = fixture.debugElement.query(By.css('#unlinkPos'));
    expect(addNewEl).toBeTruthy();
    expect(unlinkEl).toBeNull();
  });

  it('should not show Add new Position button if one or more Position from table is checked', () => {
    component.PosToUnlink = ['Test Pos', 'Test Pos 2'];
    fixture.detectChanges();
    addNewEl = fixture.debugElement.query(By.css('#addNewPos'));
    unlinkEl = fixture.debugElement.query(By.css('#unlinkPos'));
    expect(addNewEl).toBeNull();
    expect(unlinkEl).toBeTruthy();
  });


});
