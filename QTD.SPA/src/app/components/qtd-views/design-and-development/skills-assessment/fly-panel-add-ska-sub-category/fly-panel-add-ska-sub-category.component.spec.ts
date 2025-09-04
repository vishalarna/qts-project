import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddSkaSubCategoryComponent } from './fly-panel-add-ska-sub-category.component';

describe('FlyPanelAddSkaSubCategoryComponent', () => {
  let component: FlyPanelAddSkaSubCategoryComponent;
  let fixture: ComponentFixture<FlyPanelAddSkaSubCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddSkaSubCategoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddSkaSubCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
