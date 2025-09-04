import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IlaEmpTestSettingsComponent } from './ila-emp-test-settings.component';

describe('IlaEmpTestSettingsComponent', () => {
  let component: IlaEmpTestSettingsComponent;
  let fixture: ComponentFixture<IlaEmpTestSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IlaEmpTestSettingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IlaEmpTestSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
