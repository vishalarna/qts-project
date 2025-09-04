import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelReleaseTaskQualByEmpComponent } from './flypanel-release-task-qual-by-emp.component';

describe('FlypanelReleaseTaskQualByEmpComponent', () => {
  let component: FlypanelReleaseTaskQualByEmpComponent;
  let fixture: ComponentFixture<FlypanelReleaseTaskQualByEmpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelReleaseTaskQualByEmpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelReleaseTaskQualByEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
