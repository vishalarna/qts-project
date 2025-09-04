import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpDefaultComponent } from './emp-default.component';

describe('EmpDefaultComponent', () => {
  let component: EmpDefaultComponent;
  let fixture: ComponentFixture<EmpDefaultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmpDefaultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmpDefaultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
