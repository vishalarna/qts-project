import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelObjectivesComponent } from './fly-panel-objectives.component';

describe('FlyPanelObjectivesComponent', () => {
  let component: FlyPanelObjectivesComponent;
  let fixture: ComponentFixture<FlyPanelObjectivesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelObjectivesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelObjectivesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
