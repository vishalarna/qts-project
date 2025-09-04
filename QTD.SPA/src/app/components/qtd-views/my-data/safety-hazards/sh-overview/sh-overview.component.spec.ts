import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShOverviewComponent } from './sh-overview.component';

describe('ShOverviewComponent', () => {
  let component: ShOverviewComponent;
  let fixture: ComponentFixture<ShOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
