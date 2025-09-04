import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddDefinitionCategoryComponent } from './fly-panel-add-definition-category.component';

describe('FlyPanelAddDefinitionCategoryComponent', () => {
  let component: FlyPanelAddDefinitionCategoryComponent;
  let fixture: ComponentFixture<FlyPanelAddDefinitionCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddDefinitionCategoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddDefinitionCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
