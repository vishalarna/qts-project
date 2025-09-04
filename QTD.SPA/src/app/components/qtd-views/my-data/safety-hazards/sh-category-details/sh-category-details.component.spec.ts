import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShCategoryDetailsComponent } from './sh-category-details.component';

describe('ShCategoryDetailsComponent', () => {
  let component: ShCategoryDetailsComponent;
  let fixture: ComponentFixture<ShCategoryDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShCategoryDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShCategoryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
