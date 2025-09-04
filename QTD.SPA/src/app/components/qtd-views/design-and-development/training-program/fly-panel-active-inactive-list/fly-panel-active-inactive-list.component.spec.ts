import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelActiveInactiveListComponent } from './fly-panel-active-inactive-list.component';

describe('FlyPanelActiveInactiveListComponent', () => {
  let component: FlyPanelActiveInactiveListComponent;
  let fixture: ComponentFixture<FlyPanelActiveInactiveListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelActiveInactiveListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelActiveInactiveListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
