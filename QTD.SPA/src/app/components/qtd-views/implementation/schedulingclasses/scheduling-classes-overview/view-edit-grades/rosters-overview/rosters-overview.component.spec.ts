import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RostersOverviewComponent } from './rosters-overview.component';

describe('RostersOverviewComponent', () => {
  let component: RostersOverviewComponent;
  let fixture: ComponentFixture<RostersOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RostersOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RostersOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
