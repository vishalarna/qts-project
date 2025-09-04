import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddRrComponent } from './fly-panel-add-rr.component';

describe('FlyPanelAddRrComponent', () => {
  let component: FlyPanelAddRrComponent;
  let fixture: ComponentFixture<FlyPanelAddRrComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddRrComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddRrComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
