import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddLocationCategoryComponent } from './fly-panel-add-location-category.component';

describe('FlyPanelAddLocationCategoryComponent', () => {
  let component: FlyPanelAddLocationCategoryComponent;
  let fixture: ComponentFixture<FlyPanelAddLocationCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddLocationCategoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddLocationCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
