import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkReviewersComponent } from './fly-panel-link-reviewers.component';

describe('FlyPanelLinkReviewersComponent', () => {
  let component: FlyPanelLinkReviewersComponent;
  let fixture: ComponentFixture<FlyPanelLinkReviewersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkReviewersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkReviewersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
