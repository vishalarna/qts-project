import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddShSetComponent } from './fly-panel-add-sh-set.component';

describe('FlyPanelAddShSetComponent', () => {
  let component: FlyPanelAddShSetComponent;
  let fixture: ComponentFixture<FlyPanelAddShSetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddShSetComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddShSetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
