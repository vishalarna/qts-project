import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskDataElementComponent } from './fly-panel-task-data-element.component';

describe('FlyPanelTaskDataElementComponent', () => {
  let component: FlyPanelTaskDataElementComponent;
  let fixture: ComponentFixture<FlyPanelTaskDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
