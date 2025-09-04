import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelShCategoryComponent } from './fly-panel-sh-category.component';

describe('FlyPanelShCategoryComponent', () => {
  let component: FlyPanelShCategoryComponent;
  let fixture: ComponentFixture<FlyPanelShCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelShCategoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelShCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
