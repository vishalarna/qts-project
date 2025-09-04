import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefinitionCategoryDetailsComponent } from './definition-category-details.component';

describe('DefinitionCategoryDetailsComponent', () => {
  let component: DefinitionCategoryDetailsComponent;
  let fixture: ComponentFixture<DefinitionCategoryDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DefinitionCategoryDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DefinitionCategoryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
