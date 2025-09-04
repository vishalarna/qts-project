import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTestInformationComponent } from './create-test-information.component';

describe('CreateTestInformationComponent', () => {
  let component: CreateTestInformationComponent;
  let fixture: ComponentFixture<CreateTestInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateTestInformationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateTestInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
