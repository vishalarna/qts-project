import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnablingObjectivesOverviewComponent } from './enabling-objectives-overview.component';

describe('EnablingObjectivesOverviewComponent', () => {
  let component: EnablingObjectivesOverviewComponent;
  let fixture: ComponentFixture<EnablingObjectivesOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnablingObjectivesOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnablingObjectivesOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
