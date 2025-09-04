import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddToolCategoryComponent } from './fly-panel-add-tool-category.component';

describe('FlyPanelAddToolCategoryComponent', () => {
  let component: FlyPanelAddToolCategoryComponent;
  let fixture: ComponentFixture<FlyPanelAddToolCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddToolCategoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddToolCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
