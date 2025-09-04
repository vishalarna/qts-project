import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionBulkEditComponent } from './position-bulk-edit.component';

describe('PositionBulkEditComponent', () => {
  let component: PositionBulkEditComponent;
  let fixture: ComponentFixture<PositionBulkEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PositionBulkEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionBulkEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
