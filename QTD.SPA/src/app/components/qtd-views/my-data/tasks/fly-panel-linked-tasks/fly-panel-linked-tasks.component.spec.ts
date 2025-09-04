import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkedTasksComponent } from './fly-panel-linked-tasks.component';

describe('FlyPanelLinkedTasksComponent', () => {
  let component: FlyPanelLinkedTasksComponent;
  let fixture: ComponentFixture<FlyPanelLinkedTasksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkedTasksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkedTasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
