import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMatchTheColumnComponent } from './add-match-the-column.component';

describe('AddMatchTheColumnComponent', () => {
  let component: AddMatchTheColumnComponent;
  let fixture: ComponentFixture<AddMatchTheColumnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddMatchTheColumnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMatchTheColumnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
