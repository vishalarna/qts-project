import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoEditCriteriaComponent } from './flypanel-eo-edit-criteria.component';

describe('FlypanelEoEditCriteriaComponent', () => {
  let component: FlypanelEoEditCriteriaComponent;
  let fixture: ComponentFixture<FlypanelEoEditCriteriaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoEditCriteriaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoEditCriteriaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
