import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelRandomlySelectByIndividualEoComponent } from './flypanel-randomly-select-by-individual-eo.component';

describe('FlypanelRandomlySelectByIndividualEoComponent', () => {
  let component: FlypanelRandomlySelectByIndividualEoComponent;
  let fixture: ComponentFixture<FlypanelRandomlySelectByIndividualEoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelRandomlySelectByIndividualEoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelRandomlySelectByIndividualEoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
