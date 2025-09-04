import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IlaEmpSchedulingComponent } from './ila-emp-scheduling.component';

describe('IlaEmpSchedulingComponent', () => {
  let component: IlaEmpSchedulingComponent;
  let fixture: ComponentFixture<IlaEmpSchedulingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IlaEmpSchedulingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IlaEmpSchedulingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
