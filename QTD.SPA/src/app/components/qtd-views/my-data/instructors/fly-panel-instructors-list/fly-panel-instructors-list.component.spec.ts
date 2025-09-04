import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelInstructorsListComponent } from './fly-panel-instructors-list.component';

describe('FlyPanelInstructorsListComponent', () => {
  let component: FlyPanelInstructorsListComponent;
  let fixture: ComponentFixture<FlyPanelInstructorsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelInstructorsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelInstructorsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
