import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelPublicClassDetailViewComponent } from './fly-panel-public-class-detail-view.component';

describe('FlyPanelPublicClassDetailViewComponent', () => {
  let component: FlyPanelPublicClassDetailViewComponent;
  let fixture: ComponentFixture<FlyPanelPublicClassDetailViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelPublicClassDetailViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelPublicClassDetailViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
