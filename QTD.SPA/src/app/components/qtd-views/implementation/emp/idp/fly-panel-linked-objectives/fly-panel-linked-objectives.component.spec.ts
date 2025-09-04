import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkedObjectivesComponent } from './fly-panel-linked-objectives.component';

describe('FlyPanelLinkedObjectivesComponent', () => {
  let component: FlyPanelLinkedObjectivesComponent;
  let fixture: ComponentFixture<FlyPanelLinkedObjectivesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkedObjectivesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkedObjectivesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
