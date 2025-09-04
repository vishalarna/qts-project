import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IlaEmpTqSettingsComponent } from './ila-emp-tq-settings.component';

describe('IlaEmpTqSettingsComponent', () => {
  let component: IlaEmpTqSettingsComponent;
  let fixture: ComponentFixture<IlaEmpTqSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IlaEmpTqSettingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IlaEmpTqSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
