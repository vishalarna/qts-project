import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelMetaTaskDataElementComponent } from './fly-panel-meta-task-data-element.component';

describe('FlyPanelMetaTaskDataElementComponent', () => {
  let component: FlyPanelMetaTaskDataElementComponent;
  let fixture: ComponentFixture<FlyPanelMetaTaskDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelMetaTaskDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelMetaTaskDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
