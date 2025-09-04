import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEOSubCategoryComponent } from './flypanel-eo-sub-category.component';

describe('FlypanelEOSubCategoryComponent', () => {
  let component: FlypanelEOSubCategoryComponent;
  let fixture: ComponentFixture<FlypanelEOSubCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEOSubCategoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEOSubCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
