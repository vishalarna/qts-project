import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EoProceduresComponent } from './eo-procedures.component';

describe('EoProceduresComponent', () => {
  let component: EoProceduresComponent;
  let fixture: ComponentFixture<EoProceduresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EoProceduresComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EoProceduresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
