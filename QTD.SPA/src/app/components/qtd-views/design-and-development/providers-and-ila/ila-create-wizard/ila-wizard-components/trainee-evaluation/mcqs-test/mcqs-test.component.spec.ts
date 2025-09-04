import { ComponentFixture, TestBed } from '@angular/core/testing';

import { McqsTestComponent } from './mcqs-test.component';

describe('McqsTestComponent', () => {
  let component: McqsTestComponent;
  let fixture: ComponentFixture<McqsTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ McqsTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(McqsTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
