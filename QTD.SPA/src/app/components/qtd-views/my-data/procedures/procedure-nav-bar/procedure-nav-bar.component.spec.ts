import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcedureNavBarComponent } from './procedure-nav-bar.component';

describe('ProcedureNavBarComponent', () => {
  let component: ProcedureNavBarComponent;
  let fixture: ComponentFixture<ProcedureNavBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcedureNavBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcedureNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
