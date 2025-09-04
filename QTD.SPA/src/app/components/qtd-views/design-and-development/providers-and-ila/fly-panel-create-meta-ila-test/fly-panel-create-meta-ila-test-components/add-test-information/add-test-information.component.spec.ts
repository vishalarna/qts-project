import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTestInformationComponent } from './add-test-information.component';

describe('AddTestInformationComponent', () => {
  let component: AddTestInformationComponent;
  let fixture: ComponentFixture<AddTestInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTestInformationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTestInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
