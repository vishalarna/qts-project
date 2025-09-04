import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpNavBarComponent } from './emp-nav-bar.component';

describe('EmpNavBarComponent', () => {
  let component: EmpNavBarComponent;
  let fixture: ComponentFixture<EmpNavBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmpNavBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmpNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
