import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkedTrainingGroupsComponent } from './fly-panel-linked-training-groups.component';

describe('FlyPanelLinkedTrainingGroupsComponent', () => {
  let component: FlyPanelLinkedTrainingGroupsComponent;
  let fixture: ComponentFixture<FlyPanelLinkedTrainingGroupsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkedTrainingGroupsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkedTrainingGroupsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
