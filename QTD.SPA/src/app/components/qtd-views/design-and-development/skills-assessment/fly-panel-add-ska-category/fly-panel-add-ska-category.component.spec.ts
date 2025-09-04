import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddSkaCategoryComponent } from './fly-panel-add-ska-category.component';

describe('FlyPanelAddSkaCategoryComponent', () => {
  let component: FlyPanelAddSkaCategoryComponent;
  let fixture: ComponentFixture<FlyPanelAddSkaCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddSkaCategoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddSkaCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
