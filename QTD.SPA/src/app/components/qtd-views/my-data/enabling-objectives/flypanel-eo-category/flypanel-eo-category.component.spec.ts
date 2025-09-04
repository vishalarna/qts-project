import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEOCategoryComponent } from './flypanel-eo-category.component';

describe('FlypanelEOCategoryComponent', () => {
  let component: FlypanelEOCategoryComponent;
  let fixture: ComponentFixture<FlypanelEOCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEOCategoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEOCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
