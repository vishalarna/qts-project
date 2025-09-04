import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTrainingprogramLinkILaComponent } from './fly-panel-trainingprogram-link-ila.component';

describe('FlyPanelTrainingprogramLinkILaComponent', () => {
  let component: FlyPanelTrainingprogramLinkILaComponent;
  let fixture: ComponentFixture<FlyPanelTrainingprogramLinkILaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTrainingprogramLinkILaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTrainingprogramLinkILaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
