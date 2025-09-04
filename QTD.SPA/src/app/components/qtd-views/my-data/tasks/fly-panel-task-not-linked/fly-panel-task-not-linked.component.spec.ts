import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskNotLinkedComponent } from './fly-panel-task-not-linked.component';

describe('FlyPanelTaskNotLinkedComponent', () => {
  let component: FlyPanelTaskNotLinkedComponent;
  let fixture: ComponentFixture<FlyPanelTaskNotLinkedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskNotLinkedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskNotLinkedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
