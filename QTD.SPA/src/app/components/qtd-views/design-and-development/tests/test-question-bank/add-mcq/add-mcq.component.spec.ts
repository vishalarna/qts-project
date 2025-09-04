import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMcqComponent } from './add-mcq.component';

describe('AddMcqComponent', () => {
  let component: AddMcqComponent;
  let fixture: ComponentFixture<AddMcqComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddMcqComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMcqComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
