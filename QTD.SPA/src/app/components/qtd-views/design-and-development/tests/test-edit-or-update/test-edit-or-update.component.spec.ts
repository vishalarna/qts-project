import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestEditOrUpdateComponent } from './test-edit-or-update.component';

describe('TestEditOrUpdateComponent', () => {
  let component: TestEditOrUpdateComponent;
  let fixture: ComponentFixture<TestEditOrUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestEditOrUpdateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestEditOrUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
