import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IlaEmpEvalSettingsComponent } from './ila-emp-eval-settings.component';

describe('IlaEmpEvalSettingsComponent', () => {
  let component: IlaEmpEvalSettingsComponent;
  let fixture: ComponentFixture<IlaEmpEvalSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IlaEmpEvalSettingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IlaEmpEvalSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
