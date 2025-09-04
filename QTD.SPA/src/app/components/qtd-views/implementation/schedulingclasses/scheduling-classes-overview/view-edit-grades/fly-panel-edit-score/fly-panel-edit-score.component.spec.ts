import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEditScoreComponent } from './fly-panel-edit-score.component';

describe('FlyPanelEditScoreComponent', () => {
  let component: FlyPanelEditScoreComponent;
  let fixture: ComponentFixture<FlyPanelEditScoreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEditScoreComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEditScoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
