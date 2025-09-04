import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IlaEmpCbtSettingsComponent } from './ila-emp-cbt-settings.component';

describe('IlaEmpCbtSettingsComponent', () => {
  let component: IlaEmpCbtSettingsComponent;
  let fixture: ComponentFixture<IlaEmpCbtSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IlaEmpCbtSettingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IlaEmpCbtSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
