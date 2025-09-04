import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelPositionR6TaskInformationComponent } from './fly-panel-position-r6-task-information.component';

describe('FlyPanelPositionR6TaskInformationComponent', () => {
  let component: FlyPanelPositionR6TaskInformationComponent;
  let fixture: ComponentFixture<FlyPanelPositionR6TaskInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelPositionR6TaskInformationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelPositionR6TaskInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
