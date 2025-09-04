import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEditCriteriaComponent } from './fly-panel-edit-criteria.component';

describe('FlyPanelEditCriteriaComponent', () => {
  let component: FlyPanelEditCriteriaComponent;
  let fixture: ComponentFixture<FlyPanelEditCriteriaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEditCriteriaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEditCriteriaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
