import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocationCategoryDetailsComponent } from './location-category-details.component';

describe('LocationCategorydetailsComponent', () => {
  let component: LocationCategoryDetailsComponent;
  let fixture: ComponentFixture<LocationCategoryDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LocationCategoryDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LocationCategoryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
