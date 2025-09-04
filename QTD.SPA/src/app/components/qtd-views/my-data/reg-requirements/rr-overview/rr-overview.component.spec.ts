import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RrOverviewComponent } from './rr-overview.component';

describe('RrOverviewComponent', () => {
  let component: RrOverviewComponent;
  let fixture: ComponentFixture<RrOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RrOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RrOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
