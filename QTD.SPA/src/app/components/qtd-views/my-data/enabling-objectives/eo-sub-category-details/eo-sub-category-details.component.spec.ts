import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EoSubCategoryDetailsComponent } from './eo-sub-category-details.component';

describe('EoSubCategoryDetailsComponent', () => {
  let component: EoSubCategoryDetailsComponent;
  let fixture: ComponentFixture<EoSubCategoryDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EoSubCategoryDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EoSubCategoryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
