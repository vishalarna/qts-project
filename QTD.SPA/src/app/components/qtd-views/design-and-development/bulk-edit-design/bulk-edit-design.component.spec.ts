import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulkEditDesignComponent } from './bulk-edit-design.component';

describe('BulkEditDesignComponent', () => {
  let component: BulkEditDesignComponent;
  let fixture: ComponentFixture<BulkEditDesignComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BulkEditDesignComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BulkEditDesignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
