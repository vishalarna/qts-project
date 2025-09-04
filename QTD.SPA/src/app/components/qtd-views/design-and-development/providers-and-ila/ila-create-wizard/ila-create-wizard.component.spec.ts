import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IlaCreateWizardComponent } from './ila-create-wizard.component';

describe('IlaCreateWizardComponent', () => {
  let component: IlaCreateWizardComponent;
  let fixture: ComponentFixture<IlaCreateWizardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IlaCreateWizardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IlaCreateWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
