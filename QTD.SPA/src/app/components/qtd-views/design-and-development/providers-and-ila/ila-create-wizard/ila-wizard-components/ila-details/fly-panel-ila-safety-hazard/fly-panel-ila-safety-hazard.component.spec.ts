/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FlyaPanelILASafetyHazardComponent } from './fly-panel-ila-safety-hazard.component';

describe('SafetyHazardComponent', () => {
  let component: FlyaPanelILASafetyHazardComponent;
  let fixture: ComponentFixture<FlyaPanelILASafetyHazardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FlyaPanelILASafetyHazardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyaPanelILASafetyHazardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
