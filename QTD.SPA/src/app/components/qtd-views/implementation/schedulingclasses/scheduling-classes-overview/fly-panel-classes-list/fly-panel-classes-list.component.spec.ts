import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelClassesListComponent } from './fly-panel-classes-list.component';

describe('FlyPanelClassesListComponent', () => {
  let component: FlyPanelClassesListComponent;
  let fixture: ComponentFixture<FlyPanelClassesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelClassesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelClassesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
