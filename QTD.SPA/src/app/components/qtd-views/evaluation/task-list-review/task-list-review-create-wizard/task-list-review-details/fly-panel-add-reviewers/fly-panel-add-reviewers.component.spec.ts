import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddReviewersComponent } from './fly-panel-add-reviewers.component';

describe('FlyPanelAddReviewersComponent', () => {
  let component: FlyPanelAddReviewersComponent;
  let fixture: ComponentFixture<FlyPanelAddReviewersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddReviewersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddReviewersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
