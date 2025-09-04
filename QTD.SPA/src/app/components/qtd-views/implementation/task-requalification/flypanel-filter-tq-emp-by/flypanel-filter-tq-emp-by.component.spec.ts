import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelFilterTqEmpByComponent } from './flypanel-filter-tq-emp-by.component';

describe('FlypanelFilterTqEmpByComponent', () => {
  let component: FlypanelFilterTqEmpByComponent;
  let fixture: ComponentFixture<FlypanelFilterTqEmpByComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelFilterTqEmpByComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelFilterTqEmpByComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
