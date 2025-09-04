import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelReleaseTaskQualificationComponent } from './flypanel-release-task-qualification.component';

describe('FlypanelReleaseTaskQualificationComponent', () => {
  let component: FlypanelReleaseTaskQualificationComponent;
  let fixture: ComponentFixture<FlypanelReleaseTaskQualificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelReleaseTaskQualificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelReleaseTaskQualificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
