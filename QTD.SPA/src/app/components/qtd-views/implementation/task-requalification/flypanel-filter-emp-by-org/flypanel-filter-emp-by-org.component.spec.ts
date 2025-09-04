import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelFilterEmpByOrgComponent } from './flypanel-filter-emp-by-org.component';

describe('FlypanelFilterEmpByOrgComponent', () => {
  let component: FlypanelFilterEmpByOrgComponent;
  let fixture: ComponentFixture<FlypanelFilterEmpByOrgComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelFilterEmpByOrgComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelFilterEmpByOrgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
