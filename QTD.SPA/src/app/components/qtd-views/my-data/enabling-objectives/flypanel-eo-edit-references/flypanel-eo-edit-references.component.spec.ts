import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoEditReferencesComponent } from './flypanel-eo-edit-references.component';

describe('FlypanelEoEditReferencesComponent', () => {
  let component: FlypanelEoEditReferencesComponent;
  let fixture: ComponentFixture<FlypanelEoEditReferencesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoEditReferencesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoEditReferencesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
