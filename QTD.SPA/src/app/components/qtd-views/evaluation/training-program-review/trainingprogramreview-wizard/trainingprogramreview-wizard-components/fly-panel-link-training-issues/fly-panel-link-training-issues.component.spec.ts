import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkTrainingIssuesComponent } from './fly-panel-link-training-issues.component';

describe('FlyPanelLinkTrainingIssuesComponent', () => {
  let component: FlyPanelLinkTrainingIssuesComponent;
  let fixture: ComponentFixture<FlyPanelLinkTrainingIssuesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkTrainingIssuesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkTrainingIssuesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
