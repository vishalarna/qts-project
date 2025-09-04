import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestCreateWizardComponent } from './test-create-wizard.component';

describe('TestCreateWizardComponent', () => {
  let component: TestCreateWizardComponent;
  let fixture: ComponentFixture<TestCreateWizardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestCreateWizardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestCreateWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
